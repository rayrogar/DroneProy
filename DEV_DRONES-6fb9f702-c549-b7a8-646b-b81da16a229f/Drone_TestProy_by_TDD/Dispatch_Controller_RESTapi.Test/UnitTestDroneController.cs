using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter.Xml;
using Dispatch_Controller_RESTapi.Controllers;
using Dispatch_Controller_RESTapi.Controllers.Peticiones;
using Dispatch_Controller_RESTapi.Enums;
using Dispatch_Controller_RESTapi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Dispatch_Controller_RESTapi.Test
{
    public class UnitTestDroneController
    {
        [Fact]
        public async Task CallRegiterDroneEndPoint_BadRequest()
        {
            // Given
            var droneController = new DroneController();

            // When
            var result = await droneController.RegisterDrone(
                new PeticionesDrone
                {
                    Serial = "",
                    BateryState = 100,
                    WeigthLimit = 200,
                    State = "DELIVERING",
                    DroneModel = "Middleweight" 
                }
            );
            // Then
            Assert.NotNull(result);
            var response = result.Result as ActionResult;

            //Check bad request
            Assert.IsType(typeof(BadRequestObjectResult), response);

        }

        [Fact]
        public async Task CallRegisterDrone_Succesfull()
        {
            // Given
            var droneController = new DroneController();
            string newSerial = Guid.NewGuid().ToString();
            // When
            var result = await droneController.RegisterDrone(
               new PeticionesDrone
               {
                   Serial = newSerial,
                   BateryState = 100,
                   WeigthLimit = 200,
                   State = "IDLE",
                   DroneModel = "Cruiserweight"
               }
           );

            // Then
             Assert.NotNull(result);
             var response=result.Result as CreatedResult;
            Assert.Equal(201, response.StatusCode);

            var resultValue = response.Value as PeticionesDrone;

            Assert.True(resultValue.Serial == newSerial
                                             ,"The Drone returned has diferent 'Serial' value then the one sended");
             Assert.True(resultValue.BateryState == 100
                                             ,"The Drone returned has diferent 'BateryState' value then the one sended");
             Assert.True(resultValue.WeigthLimit == 200
                                             ,"The Drone returned has diferent 'WeigthLimit' value then the one sended");

            Assert.True(resultValue.State == StateEnum.IDLE.ToString()
                                             ,"The Drone returned has diferent 'Status' value then the one sended");
             Assert.True(resultValue.DroneModel == ModelEnum.Cruiserweight.ToString()
                                             ,"The Drone returned has diferent 'Model' value then the one sended");
        }
    }
}