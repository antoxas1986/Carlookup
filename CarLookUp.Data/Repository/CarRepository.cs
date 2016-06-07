using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarLookUp.Core.Models;
using CarLookUp.Data.Context.Interfaces;
using CarLookUp.Data.Entities;
using CarLookUp.Data.Repository.Interfaces;
using EntityFramework.Extensions;
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

        public Car AddCar(CarDTOWithBodyType car)
        {
            Car carEn = Mapper.Map<Car>(car);
            return _db.Cars.Add(carEn);
        }

        public void DeleteCar(int id)
        {
            _db.Cars.Where(c => c.Id == id).Delete();
        }

        public void Edit(CarDTOWithBodyType carDto)
        {
            Car car = _db.Cars.Find(carDto.Id);
            car = Mapper.Map(carDto, car);
        }

        public ICollection<T> GetAll<T>()
        {
            return _db.Cars.ProjectTo<T>().ToList();
        }

        public T GetCar<T>(int id)
        {
            return _db.Cars.Where(c => c.Id == id).ProjectTo<T>().FirstOrDefault();
        }
    }
}
