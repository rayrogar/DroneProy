using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using Dispatch_Controller_RESTapi.Controllers.Peticiones;
using Dispatch_Controller_RESTapi.Data;
using Dispatch_Controller_RESTapi.Enums;
using Dispatch_Controller_RESTapi.Logics;
using Dispatch_Controller_RESTapi.Models;
using Microsoft.Extensions.ObjectPool;
using Xunit;


namespace Dispatch_Controller_RESTapi.Test
{
    public class UnitTestDroneLogic
    {
        public UnitTestDroneLogic()
        {

        }

        [Fact]
        public async Task SaveDrone_SuccessShouldReturnDroneSaved()
        {
            // Given
            PeticionesDrone record = new PeticionesDrone
            {
                Serial = Guid.NewGuid().ToString(),
                DroneModel = ModelEnum.Cruiserweight.ToString(),
                WeigthLimit = 200,
                BateryState = 50,
                State = StateEnum.DELIVERED.ToString()

            };

            DroneLogic testObject = new DroneLogic();

            // When
            var result = await testObject.SaveDrone(record);


            // Then
            Assert.NotNull(result);
            Assert.True(result.Id != 0);
        }

        [Fact]
        public async Task SaveDrones_ThrowExceptions()
        {
            // Given
            PeticionesDrone record = new PeticionesDrone
            {
                Serial = Guid.NewGuid().ToString(),
                DroneModel = ModelEnum.Cruiserweight.ToString(),
                WeigthLimit = 501,
                BateryState = 50,
                State = StateEnum.DELIVERED.ToString()
            };

            // When
            DroneLogic testObject = new DroneLogic();

            // Then
            var e = await Record.ExceptionAsync(() => testObject.SaveDrone(record));
            Assert.NotNull(e);
            Assert.IsType<ArgumentException>(e);
        }

        [Theory]
        [MemberData(nameof(LoadFailData))]
        public async Task LoadDroneWithMedication_ThrowExceptionS(int droneId, IEnumerable<PeticionesMedications> loadList)
        {
            // Given
            DroneLogic testObject = new DroneLogic();

            // When
            var e = await Record.ExceptionAsync(() => testObject.LoadDroneById(droneId, loadList));

            // Then
            Assert.NotNull(e);
            Assert.IsType<ArgumentException>(e);

        }

        [Theory]
        [MemberData(nameof(LoadData))]
        public async Task LoadDroneWithMedication_SuccessCall(int droneId, IEnumerable<PeticionesMedications> loadList)
        {

            // Given
           
           DroneLogic testObject = new DroneLogic();

            // When
            var result = await testObject.LoadDroneById(droneId, loadList);

            // Then


            Assert.NotNull(result); //Get target Drone
            Assert.NotNull(result.Load); //Target Drone had a Load
            Assert.Equal(result.Load.Count(), loadList.Count()); // Load has same size of paramater

            var loadListDrone = result.Load;
            //Every value in same position are the same
            for (int i = 0; i < loadListDrone.Count(); ++i)
            {

                Assert.Equal(loadList.ToArray()[i].Code, loadListDrone[i].Code);
                Assert.Equal(loadList.ToArray()[i].Name, loadListDrone[i].Name);
                Assert.Equal(loadList.ToArray()[i].Weigth, loadListDrone[i].Weigth);
            }
        }

        public static IEnumerable<object[]> LoadFailData(){

            DroneLogic testObject = new DroneLogic();
            List<int> ids = new List<int>();

                var result = testObject.SaveDrone(new PeticionesDrone
                    {
                        Serial = Guid.NewGuid().ToString().ToUpper(),
                        BateryState = 100,
                        DroneModel = "Cruiserweight",
                        State = "IDLE",
                        WeigthLimit = 500
                    }).Result;

                ids.Add(result.Id);

                result = testObject.SaveDrone(new PeticionesDrone
                    {
                        Serial = Guid.NewGuid().ToString().ToUpper(),
                        BateryState = 98,
                        DroneModel = "Middleweight",
                        State = "IDLE",
                        WeigthLimit = 500
                    }).Result;

                ids.Add(result.Id);

                result = testObject.SaveDrone(new PeticionesDrone
                    {
                        Serial = Guid.NewGuid().ToString().ToUpper(),
                        BateryState = 100,
                        DroneModel = "Heavyweight",
                        State = "IDLE",
                        WeigthLimit = 300

                    }).Result;
                ids.Add(result.Id);


            return new List<object[]>(){
                //Code Fail
                new object[]{
                                ids[0],
                                new List<PeticionesMedications>{
                                            new PeticionesMedications(){
                                                         Name="Medication_1",
                                                         Code=Guid.NewGuid().ToString(),
                                                         Weigth=200,
                                                         Image="Imagen.jpg"

                                             },
                                            new PeticionesMedications(){
                                                        Name="Medication_2",
                                                        Code=Guid.NewGuid().ToString().ToUpper(),
                                                        Weigth=100,
                                                        Image=""

                                             }

                                    }
                            },
                //Max Weigth Fail
                new object[]{
                               ids[1],
                                new List<PeticionesMedications>{
                                    new PeticionesMedications(){
                                        Name="Medication_3",
                                        Code=Guid.NewGuid().ToString().ToUpper().Replace('-','_'),
                                        Weigth=530,
                                        Image="Imagen_5.jpg"
                                    }
                                }
                            },
                //More weigth than drone max weigth
                new object[]{
                                ids[2],
                                new List<PeticionesMedications>{
                                    new PeticionesMedications(){
                                        Name="Medication_1",
                                        Code=Guid.NewGuid().ToString().ToUpper().Replace('-','_'),
                                        Weigth=200,
                                        Image="Imagen_6.png"
                                    },
                                    new PeticionesMedications(){
                                        Name="Medication_3",
                                        Code=Guid.NewGuid().ToString().ToUpper().Replace('-','_'),
                                        Weigth=150,
                                        Image="Image_7.jpg"
                                    },
                                    new PeticionesMedications(){
                                        Name="Medication_4",
                                        Code=Guid.NewGuid().ToString().ToUpper().Replace('-','_'),
                                        Weigth=89,
                                        Image=""
                                    }
                                }
                            }
            };

        }

        public static IEnumerable<object[]> LoadData()
        {

             DroneLogic testObject = new DroneLogic();
            List<int> ids = new List<int>();

            var result = testObject.SaveDrone(new PeticionesDrone
                {
                    Serial = Guid.NewGuid().ToString().ToUpper(),
                    BateryState = 100,
                    DroneModel = "Cruiserweight",
                    State = "IDLE",
                    WeigthLimit = 500
                }).Result;

            ids.Add(result.Id);

            result = testObject.SaveDrone(new PeticionesDrone
                {
                    Serial = Guid.NewGuid().ToString().ToUpper(),
                    BateryState = 98,
                    DroneModel = "Middleweight",
                    State = "IDLE",
                    WeigthLimit = 400
                }).Result;

            ids.Add(result.Id);

            result = testObject.SaveDrone(new PeticionesDrone
                {
                    Serial = Guid.NewGuid().ToString().ToUpper(),
                    BateryState = 100,
                    DroneModel = "Heavyweight",
                    State = "IDLE",
                    WeigthLimit = 500

                }).Result;
            ids.Add(result.Id);


            return new List<object[]>{
                    new object[]{
                                    ids[0],
                                    new List<PeticionesMedications>{
                                                new PeticionesMedications(){
                                                            Name="Medication_1",
                                                            Code=Guid.NewGuid().ToString().ToUpper().Replace('-','_'),
                                                            Weigth=200,
                                                            Image="Imagen.jpg"

                                                },
                                                new PeticionesMedications(){
                                                            Name="Medication_2",
                                                            Code=Guid.NewGuid().ToString().ToUpper().Replace('-','_'),
                                                            Weigth=100,
                                                            Image=""

                                                }

                                        }
                                },
                    new object[]{
                                    ids[1],
                                    new List<PeticionesMedications>{
                                        new PeticionesMedications(){
                                            Name="Medication_3",
                                            Code=Guid.NewGuid().ToString().ToUpper().Replace('-','_'),
                                            Weigth=200,
                                            Image="Imagen_5.jpg"
                                        }
                                    }
                                },
                    new object[]{
                                    ids[2],
                                    new List<PeticionesMedications>{
                                        new PeticionesMedications(){
                                            Name="Medication_1",
                                            Code=Guid.NewGuid().ToString().ToUpper().Replace('-','_'),
                                            Weigth=200,
                                            Image="Imagen_6.png"
                                        },
                                        new PeticionesMedications(){
                                            Name="Medication_3",
                                            Code=Guid.NewGuid().ToString().ToUpper().Replace('-','_'),
                                            Weigth=150,
                                            Image="Image_7.jpg"
                                        },
                                        new PeticionesMedications(){
                                            Name="Medication_4",
                                            Code=Guid.NewGuid().ToString().ToUpper().Replace('-','_'),
                                            Weigth=100,
                                            Image=""
                                        }
                                    }
                                }
            };
        }
    }
}
