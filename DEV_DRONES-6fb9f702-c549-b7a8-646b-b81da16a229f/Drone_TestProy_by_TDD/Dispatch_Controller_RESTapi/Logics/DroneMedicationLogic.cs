using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dispatch_Controller_RESTapi.Models;

namespace Dispatch_Controller_RESTapi.Logics
{
    public class DroneMedicationLogic
    {
        public async Task<bool> SaveDroneMedication(int droneId, int medicationId){

            bool output = false;
            using (var context = new DataContext()){
                var existDrone = await context.Drones.FindAsync(droneId);
                var existMedication = await context.Medications.FindAsync(medicationId);

                if(existDrone!=null && existMedication!=null){

                    DroneMedicationsEntity record = new DroneMedicationsEntity() { DroneId = existDrone.Id, MedicationId = existMedication.ID };
                    context.Add(record);
                    await context.SaveChangesAsync();
                    output = true;
                }
            }
            return output;
        }
    }
}