using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dispatch_Controller_RESTapi.Controllers.Peticiones;
using Dispatch_Controller_RESTapi.Models;

namespace Dispatch_Controller_RESTapi.Logics
{
    public class MedicationLogic
    {
        public async Task<PeticionesMedications> GetMedicationByIdOrCode(int Id){

            using(var context =new DataContext()){

                var medication = await context.FindAsync<MedicationEntity>(Id);
                if(medication!=null){
                    return new PeticionesMedications()
                    {
                        ID = medication.ID,
                        Code = medication.Code,
                        Name = medication.Name,
                        Weigth = medication.Weigth
                    };
                }
                else
                    return null;
            }
            
        }

        public  PeticionesMedications? GetMedicationByIdOrCode(string code){

            using(var context =new DataContext()){

                var medication = context.Medications.Where(x => x.Code == code).ToArray();
                if(medication!=null && medication.Count()!=0){
                    return new PeticionesMedications()
                    {
                        ID = medication[0].ID,
                        Code = medication[0].Code,
                        Name = medication[0].Name,
                        Weigth = medication[0].Weigth
                    };
                }
                else
                    return null;
            }
            
        }
    
        public async Task<PeticionesMedications> SaveMedication(PeticionesMedications record)
        {
            
            //Check Medication Name
            if(!Validators.CheckMedicationName(record.Name))
                throw new ArgumentException("Medication name could only have ‘letters’, ‘numbers’, ‘-’, ‘_’");

            //Check Medication Code
            if (!Validators.CheckMedicationCode(record.Code))
                throw new ArgumentException("Medication Code could only have ‘Upper case letters’, ‘underscore’ and ‘numbers’");

            MedicationEntity registro = new MedicationEntity
                                                            {
                                                                Code = record.Code,
                                                                Name = record.Name,
                                                                Weigth = record.Weigth,
                                                                Image = record.Image
                                                            };
                
            using(var context =new DataContext()){
                
                context.Add(registro);
                await context.SaveChangesAsync();
            }

            PeticionesMedications? output = registro.ID == 0 ? null
                                : new PeticionesMedications
                                        {
                                            ID = registro.ID,
                                            Code = registro.Code,
                                            Name = registro.Name,
                                            Weigth = registro.Weigth
                                        };
            return output;
        }

    }
}