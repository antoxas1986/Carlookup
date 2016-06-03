using CarLookUp.Core.Models;
using System.Collections.Generic;

namespace CarLookUp.Data.Repository.Interfaces
{
    public interface ICarRepository
    {
        /// <summary>
        /// Adds the car.
        /// </summary>
        /// <param name="car">The car.</param>
        void AddCar(CarDTO car);

        /// <summary>
        /// Deletes the car.
        /// </summary>
        /// <param name="car">The car.</param>
        void DeleteCar(CarDTO car);

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
        CarDTOWithBodyTypeName GetCar(int id);
    }
}
