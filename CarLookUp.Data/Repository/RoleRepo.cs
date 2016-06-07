using AutoMapper.QueryableExtensions;
using CarLookUp.Core.Models;
using CarLookUp.Data.Context.Interfaces;
using CarLookUp.Data.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CarLookUp.Data.Repository
{
    public class RoleRepo : IRoleRepo
    {
        private ICarContext _db;

        public RoleRepo(ICarContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Gets all roles.
        /// </summary>
        /// <returns></returns>
        public ICollection<RoleDTO> GetAll()
        {
            return _db.Roles.ProjectTo<RoleDTO>().ToList();
        }

        /// <summary>
        /// Gets the role by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public RoleDTO GetById(int id)
        {
            return _db.Roles.Where(r => r.Id == id).ProjectTo<RoleDTO>().FirstOrDefault();
        }
    }
}
