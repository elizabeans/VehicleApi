using Microsoft.VisualStudio.TestTools.UnitTesting;
using MitchellVehicleApi.Controllers;
using MitchellVehicleApi.Data.Database;
using MitchellVehicleApi.Data.Model;
using MitchellVehicleApi.Data.Repository;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;

namespace MitchellVehicleApi.Test.Controllers
{
    [TestClass]
    public class VehicleControllerTest
    {
        [TestMethod]
        public void GetVehiclesShouldReturnAllVehicles()
        {
            var mockVehicleRepo = new Mock<IVehicleRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockVehicleRepo.Setup(v => v.GetAll()).Returns(new List<Vehicle>
            {
                new Vehicle
                {
                    Id = 1,
                    Year = 2014,
                    Make = "Honda",
                    Model = "Accord"
                },

                new Vehicle
                {
                    Id = 2,
                    Year = 2003,
                    Make = "Toyota",
                    Model = "Corolla"
                },

                new Vehicle
                {
                    Id = 3,
                    Year = 1994,
                    Make = "Hyundai",
                    Model = "Elantra"
                }
            });

            var controller = new VehiclesController(mockVehicleRepo.Object, mockUnitOfWork.Object);

            var vehicles = controller.GetVehicles();

            Assert.IsNotNull(vehicles);
            Assert.IsTrue(vehicles.Count() == 3);

        }

        [TestMethod]
        public void GetVehicleShouldReturnVehicle()
        {
            var mockVehicleRepo = new Mock<IVehicleRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockVehicleRepo.Setup(x => x.GetById(24))
                .Returns(new Vehicle { Id = 24 });

            var controller = new VehiclesController(mockVehicleRepo.Object, mockUnitOfWork.Object);

            IHttpActionResult actionResult = controller.GetVehicle(24);
            var contentResult = actionResult as OkNegotiatedContentResult<Vehicle>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(24, contentResult.Content.Id);
        }

        [TestMethod]
        public void GetVehicleShouldReturnNotFoundIfVehicleDoesNotExist()
        {
            var mockVehicleRepo = new Mock<IVehicleRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var controller = new VehiclesController(mockVehicleRepo.Object, mockUnitOfWork.Object);

            var response = controller.GetVehicle(1);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PutVehicleShouldReturnNoContent()
        {
            var mockVehicleRepo = new Mock<IVehicleRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockVehicleRepo.Setup(v => v.GetById(1))
                .Returns(
                    new Vehicle
                    {
                        Id = 1,
                        Year = 2015,
                        Make = "Honda",
                        Model = "Civic"
                    }
                );

            var controller = new VehiclesController(mockVehicleRepo.Object, mockUnitOfWork.Object);

            IHttpActionResult actionResult = controller.PutVehicle(new Vehicle { Id = 1, Year = 1999, Make = "BMW", Model = "X3" });
            var contentResult = actionResult as StatusCodeResult;

            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.NoContent, contentResult.StatusCode);
        }

        [TestMethod]
        public void PostVehicleSetsLocation()
        {
            var mockVehicleRepo = new Mock<IVehicleRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var controller = new VehiclesController(mockVehicleRepo.Object, mockUnitOfWork.Object);

            IHttpActionResult actionResult = controller.PostVehicle(new Vehicle { Id = 1, Year = 2000, Make = "Tonka", Model = "Truck" });
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Vehicle>;

            Assert.IsNotNull(createdResult);
            Assert.AreEqual("DefaultApi", createdResult.RouteName);
            Assert.AreEqual(1, createdResult.RouteValues["id"]);
        }

        [TestMethod]
        public void DeleteVehicleReturnsOk()
        {
            var mockVehicleRepo = new Mock<IVehicleRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockVehicleRepo.Setup(v => v.GetById(100))
               .Returns(
                   new Vehicle
                   {
                       Id = 100,
                       Year = 2015,
                       Make = "Honda",
                       Model = "Civic"
                   }
               );

            var controller = new VehiclesController(mockVehicleRepo.Object, mockUnitOfWork.Object);

            IHttpActionResult actionResult = controller.DeleteVehicle(100);
            var contentResult = actionResult as OkNegotiatedContentResult<Vehicle>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(100, contentResult.Content.Id);
        }
    }
}
