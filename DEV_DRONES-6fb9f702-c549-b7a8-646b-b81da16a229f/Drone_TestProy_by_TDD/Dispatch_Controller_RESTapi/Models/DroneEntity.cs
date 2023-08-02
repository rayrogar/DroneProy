using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Dispatch_Controller_RESTapi.Enums;

namespace Dispatch_Controller_RESTapi.Models
{
    public class DroneEntity
    {
        [Key]
        public int Id{ get; set; }
        [MaxLength(100)]
        [Required]
        public string Serial { get; set; }

        public int WeigthLimit{ get; set; }

        public int BateryState { get; set; }

        public int StateId { get; set; }
        //public virtual StateEntity State { get; set; }
        public int ModelId { get; set; }
        //public virtual ModelEntity DroneModel { get; set; }
        public virtual ICollection<MedicationEntity> Carga { get; set; }
    }
}