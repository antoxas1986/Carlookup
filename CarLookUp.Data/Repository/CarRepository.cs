using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarLookUp.Core.Constants;
using CarLookUp.Core.Enum;
using CarLookUp.Core.Models;
using CarLookUp.Data.Context.Interfaces;
using CarLookUp.Data.Entities;
using CarLookUp.Data.Repository.Interfaces;
using EntityFramework.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace CarLookUp.Data.Repository
{
    /// <summary>
    /// Implementation of car repository interface.
    /// </summary>
    /// <seealso cref="CarLookUp.Data.Repository.Interfaces.ICarRepository" />
    public class CarRepository : ICarRepository
    {
        private ICarContext _db;

        public CarRepository(ICarContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Adds the car.
        /// </summary>
        /// <param name="car">The car.</param>
        /// <returns></returns>
        public void AddCar(CarDTOWithBodyType car)
        {
            Car carEn = Mapper.Map<Car>(car);
            _db.Cars.Add(carEn);
        }

        /// <summary>
        /// Deletes the car by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteCar(int id)
        {
            _db.Cars.Where(c => c.Id == id).Delete();
        }

        /// <summary>
        /// Edits the specified car dto.
        /// </summary>
        /// <param name="carDto">The car dto.</param>
        public void Edit(CarDTOWithBodyType carDto, ValidationMessageList messages)
        {
            Car car = _db.Cars.Find(carDto.Id);
            if (car == null)
            {
                messages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_CAR));
                return;
            }
            car = Mapper.Map(carDto, car);
        }

        /// <summary>
        /// Gets all cars.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ICollection<T> GetAll<T>()
        {
            return _db.Cars.ProjectTo<T>().ToList();
        }

        /// <summary>
        /// Gets the car by identifier.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public T GetCar<T>(int id)
        {
            return _db.Cars.Where(c => c.Id == id).ProjectTo<T>().FirstOrDefault();
        }
    }
}
