using AutoMapper.QueryableExtensions;
using CarLookUp.Data.Context.Interfaces;
using CarLookUp.Data.Entities;
using CarLookUp.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarLookUp.Data.Repository
{
    public class BodyTypeRepository : IBodyTypeRepository
    {
        private ICarContext _db;

        public BodyTypeRepository(ICarContext db)
        {
            _db = db;
        }

        public ICollection<T> GetAll<T>()
        {
            return _db.BodyTypes.ProjectTo<T>().ToList();
        }
    }
}
