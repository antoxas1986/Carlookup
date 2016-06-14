using AutoMapper.QueryableExtensions;
using CarLookUp.Core.Models;
using CarLookUp.Data.Context.Interfaces;
using CarLookUp.Data.Entities;
using CarLookUp.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarLookUp.Data.Repository
{
    /// <summary>
    /// Implementation bodytype interface
    /// </summary>
    /// <seealso cref="CarLookUp.Data.Repository.Interfaces.IBodyTypeRepository" />
    public class BodyTypeRepository : IBodyTypeRepository
    {
        private ICarContext _db;

        public BodyTypeRepository(ICarContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Gets all bodytypes model.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ICollection<BodyTypeDTO> GetAll()
        {
            return _db.BodyTypes.ProjectTo<BodyTypeDTO>().ToList();
        }

        /// <summary>
        /// Gets the bodytype by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public BodyTypeDTO GetById(int id)
        {
            return _db.BodyTypes.Where(b => b.Id == id).ProjectTo<BodyTypeDTO>().FirstOrDefault();
        }
    }
}
