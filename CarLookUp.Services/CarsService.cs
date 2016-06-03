using AutoMapper;
using CarLookUp.Core.Models;
using CarLookUp.Data.Entities;
using CarLookUp.Data.Repository.Interfaces;
using CarLookUp.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace CarLookUp.Services.CarServices
{
    /// <summary>
    /// Service to interact with in memory cars collection
    /// </summary>
    /// <seealso cref="CarLookUp.Services.CarServices.ICarsService" />
    public class CarsService : ICarsService
    {
        private IBodyTypeRepository _bodyTypeRepo;
        private ICarRepository _carsRepo;

        public CarsService(ICarRepository carsRepo, IBodyTypeRepository bodyTypeRepo)
        {
            _carsRepo = carsRepo;
            _bodyTypeRepo = bodyTypeRepo;
        }

        /// <summary>
        /// Adds the car.
        /// </summary>
        /// <param name="car">The car.</param>
        public void AddCar(CarDTO car)
        {
            //_carsRepo.AddCar(car);
        }

        /// <summary>
        /// Deletes the car by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteCar(int id)
        {
            //CarDTO car = _carsRepo.GetCar(id);
            //_carsRepo.DeleteCar(car);
        }

        /// <summary>
        /// Gets all cars.
        /// </summary>
        /// <returns></returns>
        public ICollection<T> GetAll<T>()
        {
            return _carsRepo.GetAll<T>();
        }

        public ICollection<T> GetAllBodyTypes<T>()
        {
            return _bodyTypeRepo.GetAll<T>();
        }

        /// <summary>
        /// Gets the car by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public CarDTOWithBodyTypeName GetCar(int id)
        {
            return _carsRepo.GetCar(id);
        }
    }
}
