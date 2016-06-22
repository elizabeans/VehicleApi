using MitchellVehicleApi.Data.Database;
using MitchellVehicleApi.Data.Model;
using MitchellVehicleApi.Data.Repository;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace MitchellVehicleApi.Controllers
{
    public class VehiclesController : ApiController
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public VehiclesController(IVehicleRepository vehicleRepository, IUnitOfWork unitOfWork)
        {
            _vehicleRepository = vehicleRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/vehicles
        public IEnumerable<Vehicle> GetVehicles()
        {
            return _vehicleRepository.GetAll();
        }

        // GET: api/vehicles/{id}
        public IHttpActionResult GetVehicle(int id)
        {
            Vehicle vehicle = _vehicleRepository.GetById(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return Ok(vehicle);
        }

        // PUT: api/vehicles/{id}
        public IHttpActionResult PutVehicle(Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var validationResult = ValidatePayload(vehicle);

            if (validationResult != null)
            {
                return validationResult;
            }

            _vehicleRepository.Update(vehicle);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!VehicleExists(vehicle.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult PostVehicle(Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var validationResult = ValidatePayload(vehicle);

            if (validationResult != null)
            {
                return validationResult;
            }

            _vehicleRepository.Add(vehicle);
            _unitOfWork.Commit();

            return CreatedAtRoute("DefaultApi", new { id = vehicle.Id }, vehicle);
        }

        public IHttpActionResult DeleteVehicle(int id)
        {
            Vehicle vehicle = _vehicleRepository.GetById(id);
            if(vehicle == null)
            {
                return NotFound();
            }

            _vehicleRepository.Delete(vehicle);
            _unitOfWork.Commit();

            return Ok(vehicle);
        }

        private bool VehicleExists(int id)
        {
            return _vehicleRepository.Any(e => e.Id == id);
        }

        private IHttpActionResult ValidatePayload(Vehicle vehicle)
        {
            if (string.IsNullOrWhiteSpace(vehicle.Make) || string.IsNullOrWhiteSpace(vehicle.Model))
            {
                return BadRequest("Year, Make, and Model are required.");
            }

            if (vehicle.Year < 1950 || vehicle.Year > 2050)
            {
                return BadRequest("Vehicle year must be between 1950 and 2050.");
            }

            return null;
        }
    }
}
