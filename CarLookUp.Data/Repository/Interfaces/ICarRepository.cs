using CarLookUp.Core.Models;
using CarLookUp.Data.Entities;
using System.Collections.Generic;

namespace CarLookUp.Data.Repository.Interfaces
{
    public interface ICarRepository
    {
        /// <summary>
        /// Adds the car.
        /// </summary>
        /// <param name="car">The car.</param>
        Car AddCar(CarDTOWithBodyType car);

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

        /// <summary>
        /// Gets the car by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        T GetCar<T>(int id);
    }
}
