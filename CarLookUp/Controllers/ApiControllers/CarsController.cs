using CarLookUp.Core.Models;
using CarLookUp.Services.Interfaces;
using CarLookUp.Web.Filters;
using CarLookUp.Web.ViewModels;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarLookUp.Web.Controllers.ApiContollers
{
    /// <summary>
    ///Controller for cars
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [ApiAuthorization]
    public class CarsController : ApiController
    {
        private static ICarsService _carsService;

        public CarsController(ICarsService carService)
        {
            _carsService = carService;
        }

        /// <summary>
        /// Deletes car by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public HttpResponseMessage Delete(int id)
        {
            _carsService.DeleteCar(id);
            return Request.CreateResponse(HttpStatusCode.OK, "Car deleted");
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CarDTO> Get()
        {
            return _carsService.GetAll<CarDTO>();
        }

        /// <summary>
        /// Gets car by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public HttpResponseMessage Get(int id)
        {
            ValidationMassageList messages = new ValidationMassageList();
            CarDTO car = _carsService.GetCar(id, messages);
            if (car != null)
            {
                return Request.CreateResponse(car);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Car not found with Id = " + id);
        }

        /// <summary>
        /// Posts the specified car.
        /// </summary>
        /// <param name="car">The car.</param>
        /// <returns></returns>
        public HttpResponseMessage Post(CarVM car)
        {
            if (ModelState.IsValid)
            {
                var newCar = new CarDTOWithBodyType();
                newCar.Maker = car.Maker;
                newCar.Model = car.Model;
                newCar.Year = car.Year;
                _carsService.AddCar(newCar);
            }
            return Request.CreateResponse("Car added");
        }

        /// <summary>
        /// Puts car by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="car">The car.</param>
        /// <returns></returns>
        public HttpResponseMessage Put(int id, CarVM car)
        {
            ValidationMassageList messages = new ValidationMassageList();
            var newCar = _carsService.GetCar(id, messages);
            if (newCar != null && ModelState.IsValid)
            {
                newCar.Maker = car.Maker;
                newCar.Model = car.Model;
                newCar.Year = car.Year;
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Car updated");
        }
    }
}
