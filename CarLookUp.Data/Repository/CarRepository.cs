using AutoMapper.QueryableExtensions;
using CarLookUp.Core.Models;
using CarLookUp.Data.Context.Interfaces;
using CarLookUp.Data.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CarLookUp.Data.Repository
{
    public class CarRepository : ICarRepository
    {
        private ICarContext _db;

        public CarRepository(ICarContext db)
        {
            _db = db;
        }

        public void AddCar(CarDTO car)
        {
            //_db.Cars.Add(car);
            _db.SaveChanges();
        }

        public void DeleteCar(CarDTO car)
        {
            //_db.Cars.Remove(car);
            _db.SaveChanges();
        }

        public ICollection<T> GetAll<T>()
        {
            return _db.Cars.ProjectTo<T>().ToList();
        }

        public CarDTOWithBodyTypeName GetCar(int id)
        {
            var car = _db.Cars.Where(c => c.Id == id).ProjectTo<CarDTOWithBodyTypeName>().FirstOrDefault();
            return car;
        }
    }
}
