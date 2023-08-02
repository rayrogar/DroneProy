using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Dispatch_Controller_RESTapi.Controllers.Peticiones;
using Dispatch_Controller_RESTapi.Logics;
//using Dispatch_Controller_RESTapi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace Dispatch_Controller_RESTapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DroneController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<PeticionesDrone>> RegisterDrone([FromBody]PeticionesDrone drone){
            try
            {
                DroneLogic droneLogic = new DroneLogic();
                var result = await droneLogic.SaveDrone(drone);
                if(result == null)
                    return StatusCode(500, "Something went wrong, please contact the administrators");

                return Created("", result);
            }
            catch (ArgumentException ae)
            {
                if(ae.Message.Contains("same serial"))
                    return StatusCode(409, ae.Message);

                return BadRequest(ae.Message);
            }
            catch(Exception e){
                return StatusCode(500, $"Something went wrong, please contact the administrators");
            }
        }
    }
}