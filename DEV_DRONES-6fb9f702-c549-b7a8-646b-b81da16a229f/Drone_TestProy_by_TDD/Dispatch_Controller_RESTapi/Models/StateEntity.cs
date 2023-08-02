using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_Controller_RESTapi.Models
{
    public class StateEntity
    {
        [Key]
        public int Id{ get; set; }
        
        [Required,MaxLength(10)]
        public string Name{ get; set; }

    }
}