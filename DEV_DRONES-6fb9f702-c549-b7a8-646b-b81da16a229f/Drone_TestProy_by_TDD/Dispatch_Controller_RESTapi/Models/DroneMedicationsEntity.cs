using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_Controller_RESTapi.Models
{
    public class DroneMedicationsEntity
    {
        public int DroneId { get; set; }
        public int MedicationId { get; set; }

        public virtual DroneEntity Drone { get; set; }
        public virtual MedicationEntity Medication { get; set; }
    }
}