using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Dispatch_Controller_RESTapi.Controllers.Peticiones;
using Dispatch_Controller_RESTapi.Data;
using Dispatch_Controller_RESTapi.Enums;
using Dispatch_Controller_RESTapi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Dispatch_Controller_RESTapi.Logics
{
    public class DroneLogic
    {

        public Task<List<PeticionesDrone>> GetsAllDrones()
        {
            throw new NotImplementedException();
        }

        public async Task<PeticionesDrone> SaveDrone(PeticionesDrone record)
        {
            //Check Serial Length
            if (string.IsNullOrEmpty(record.Serial) || record.Serial.Length > 100)
                throw new ArgumentException(record.Serial.Length > 100 ? "The serial length has to be less than 100"
                                                                    : "The serial can't be null or empty");

            //Check weigth limit
            if (record.WeigthLimit > 500)
                throw new ArgumentException("The weight limit has to be less than 500");

            //Check Batery State
            if(record.BateryState < 0 || record.BateryState > 100)
                throw new ArgumentException("Batery level most be between 0 and 100");

            PeticionesDrone? output = new PeticionesDrone();
            DroneEntity registro = new DroneEntity();

            //Check Drone Model
            registro.ModelId = (int)Enum.GetValues<ModelEnum>().Where(x => x.ToString() == record.DroneModel).ToArray()[0];
            if (registro.ModelId == null || registro.ModelId == 0)
                throw new ArgumentException($"Invalid drone model: '{record.DroneModel}' ");

            //Check Drone State
            registro.StateId = (int)Enum.GetValues<StateEnum>().Where(x => x.ToString() == record.State).ToArray()[0];
            if (registro.StateId == null || registro.StateId == 0)
                throw new ArgumentException($"Invalid drone model: '{record.State}' ");

            registro.Serial = record.Serial;
            registro.WeigthLimit = record.WeigthLimit;
            registro.BateryState = record.BateryState;


            using (var context = new DataContext())
            {
                //Check if exist a record with same serial
                var exist = context.Drones.Select(x=>x.Serial==record.Serial);
                if (exist != null && exist.Count(x=>x==true)!=0)
                    throw new ArgumentException($"Exist an object with same serial");

                context.Add(registro);
                    await context.SaveChangesAsync();

            }

            if(registro.Id==0)
                output = null;
                else
                    {
                        output.Id=registro.Id;
                        output.BateryState = registro.BateryState;
                        output.WeigthLimit = registro.WeigthLimit;
                        output.Serial = registro.Serial;
                        output.State = Enum.GetValues<StateEnum>().Where(x => (int)x == registro.StateId).ToArray()[0].ToString();
                        output.DroneModel = Enum.GetValues<ModelEnum>().Where(x => (int)x == registro.ModelId).ToArray()[0].ToString();
            }
            return output;
        }

        public async Task<PeticionesDrone> LoadDroneById(int droneId, IEnumerable<PeticionesMedications> loadList)
        {

            //Check Size of the List
            if(loadList == null || loadList.Count()==0)
                throw new ArgumentException("The list of medications must has at least one element");

            DroneEntity? droneTarget = null;

            using(var context = new DataContext()){
                //Verify if drone exist
                droneTarget = await context.Drones.FindAsync(droneId);

                if(droneTarget == null)
                    throw new ArgumentException($"Drone target doesn't exist");

                //Verifiying capacity of the target Drone
                int LoadWeigth = loadList.Sum(x => x.Weigth);
                if(droneTarget.WeigthLimit < LoadWeigth)
                    throw new ArgumentException($"Target drone with capacity of {droneTarget.WeigthLimit} can't carry selected Load of {LoadWeigth} weigth");

                //Verifiying charge of batery
                if(droneTarget.BateryState <= 25)
                    throw new Exception("Batery Level is too low");

                //Verifiying state IDLE of the Drone
                if (droneTarget.StateId == (int)StateEnum.IDLE)
                {
                    //Change Drone State to LOADING
                    droneTarget.StateId = (int)StateEnum.LOADING;
                    context.Entry(droneTarget).State=EntityState.Modified;
                    await context.SaveChangesAsync();
                }
                else
                    throw new ArgumentException($"Target Drone is un {Enum.GetValues<StateEnum>().Where(x => (int)x == droneTarget.StateId).ToArray()[0].ToString()}");
            }
            
            
            MedicationLogic medicationLogic = new MedicationLogic();
            
            PeticionesDrone output = new PeticionesDrone()
            {
                    Id=droneTarget.Id,
                    BateryState=droneTarget.BateryState,
                    Serial=droneTarget.Serial,
                    WeigthLimit = droneTarget.WeigthLimit,
                    DroneModel = Enum.GetValues<ModelEnum>().Where(x => (int)x == droneTarget.ModelId).ToArray()[0].ToString(),
                    State = Enum.GetValues<StateEnum>().Where(x => (int)x == droneTarget.StateId).ToArray()[0].ToString(),
                    Load = new List<PeticionesMedications>()
            };

            List<(int targetDroneId, int medicationTargetId)> recordSaved = new List<(int, int)>();

            foreach (var medicationItem in loadList)
                    {
                        //Verify if Medications Exist, if is not Add medication to data base
                        PeticionesMedications? record = null;
                        if(medicationItem.ID!=0)
                            record = await medicationLogic.GetMedicationByIdOrCode(medicationItem.ID);
                        else
                            record = medicationLogic.GetMedicationByIdOrCode(medicationItem.Code);

                        if(record==null)
                            record = await medicationLogic.SaveMedication(medicationItem);

                        DroneMedicationLogic droneMedicationLogic = new DroneMedicationLogic();
                        try{
                            //Change this for a method to add range
                            bool result = await droneMedicationLogic.SaveDroneMedication(droneId, record.ID);
                            if (result)
                                {
                                    output.Load.Add(record);
                                    recordSaved.Add((targetDroneId: droneId, medicationTargetId: record.ID));
                                }
                                else
                                    {
                                        //Roll Back
                                        await RollBackSaveDroneMedication(recordSaved);
                                        throw new ArgumentException("Internal Error, the operation was cancelled and change was unmade");
                                    }
                        }
                        catch(Exception e){
                            //Roll Back
                            await RollBackSaveDroneMedication(recordSaved);
                            throw new ArgumentException($"Internal Error, the operation was cancelled and change was unmade. InnerException: {e.Message}");
                }
            }

            using(var context = new DataContext()){

                //Change Drone State to LOADED
                droneTarget.StateId = (int)StateEnum.LOADED;
                context.Entry(droneTarget).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }

            //Add State for output to LOADED for feedback
            output.State = StateEnum.LOADED.ToString();

            return output;
        }

        static async Task RollBackSaveDroneMedication(List<(int targetDroneId, int medicationTargetId)> recordSaved) {

            List<DroneMedicationsEntity> toRemove = new List<DroneMedicationsEntity>();

                for (int i = 0; i < recordSaved.Count(); i++)
                {
                    toRemove.Add(new DroneMedicationsEntity()
                    {
                        DroneId = recordSaved[i].targetDroneId,
                        MedicationId = recordSaved[i].medicationTargetId
                    });
                }

                using (var context = new DataContext())
                {
                    context.RemoveRange(toRemove);
                    await context.SaveChangesAsync();
            }

        }

    }
}