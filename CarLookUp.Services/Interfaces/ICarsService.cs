﻿using CarLookUp.Core.Models;
using System.Collections.Generic;

namespace CarLookUp.Services.Interfaces
{
    public interface ICarsService
    {
        /// <summary>
        /// Adds the car.
        /// </summary>
        /// <param name="car">The car.</param>
        void AddCar(CarDTOWithBodyType car);

        /// <summary>
        /// Deletes the car.
        /// </summary>
        /// <param name="car">The car.</param>
        void DeleteCar(int id);

        void Edit(CarDTOWithBodyType carDto);

        /// <summary>
        /// Gets all cars.
        /// </summary>
        /// <returns></returns>
        ICollection<T> GetAll<T>();

        ICollection<T> GetAllBodyTypes<T>();

        /// <summary>
        /// Gets the car by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        CarDTOWithBodyType GetCar(int id);
    }
}
