using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_Controller_RESTapi.Models
{
    public class MedicationEntity
    {
        [Key]
        public int ID{ get; set; }
        public required string Name{ get; set; }
        public required int Weigth{ get; set; }
        public required string Code{ get; set; }
        public string Image{ get; set; }
    }
}