using CarLookUp.Core.Models;
using CarLookUp.Data.Entities;
using System.Collections.Generic;

namespace CarLookUp.Data.Repository.Interfaces
{
    /// <summary>
    /// Api to work with car model
    /// </summary>
    public interface ICarRepository
    {
        /// <summary>
        /// Adds the car.
        /// </summary>
        /// <param name="car">The car.</param>
        /// <returns></returns>
        void AddCar(CarDTOWithBodyType car);

        /// <summary>
        /// Deletes the car by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteCar(int id);

        /// <summary>
        /// Edits the specified car dto.
        /// </summary>
        /// <param name="carDto">The car dto.</param>
        /// <param name="messages">The messages.</param>
        void Edit(CarDTOWithBodyType carDto, ValidationMessageList messages);

        /// <summary>
        /// Gets all cars.
        /// </summary>
        /// <returns></returns>
        ICollection<T> GetAll<T>();

        /// <summary>
        /// Gets the car by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        T GetCar<T>(int id);
    }
}
