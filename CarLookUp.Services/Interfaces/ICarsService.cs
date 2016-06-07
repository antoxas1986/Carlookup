using CarLookUp.Core.Models;
using System.Collections.Generic;

namespace CarLookUp.Services.Interfaces
{
    /// <summary>
    /// Api to work with car model in service layer
    /// </summary>
    public interface ICarsService
    {
        /// <summary>
        /// Adds the car.
        /// </summary>
        /// <param name="car">The car.</param>
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
        void Edit(CarDTOWithBodyType carDto);

        /// <summary>
        /// Gets all cars.
        /// </summary>
        /// <returns></returns>
        ICollection<T> GetAll<T>();

        /// <summary>
        /// Gets all body types.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        ICollection<T> GetAllBodyTypes<T>();

        /// <summary>
        /// Gets the car by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        CarDTOWithBodyType GetCar(int id, ValidationMassageList messages);
    }
}
