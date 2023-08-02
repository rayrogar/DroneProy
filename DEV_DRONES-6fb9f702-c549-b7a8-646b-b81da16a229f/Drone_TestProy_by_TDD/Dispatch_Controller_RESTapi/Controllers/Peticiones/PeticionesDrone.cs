using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_Controller_RESTapi.Controllers.Peticiones
{
    public class PeticionesDrone
    {
        public int Id{ get; set; }
        public string Serial { get; set; }
        public string DroneModel { get; set; }
        public int WeigthLimit{ get; set; }
        public int BateryState { get; set; }
        public  string State { get; set; }
        public List<PeticionesMedications> Load{ get; set; }
    }

}