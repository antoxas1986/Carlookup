using AutoMapper.QueryableExtensions;
using CarLookUp.Core.Models;
using CarLookUp.Data.Context.Interfaces;
using CarLookUp.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLookUp.Data.Repository
{
    public class RoleRepo : IRoleRepo
    {
        private ICarContext _db;

        public RoleRepo(ICarContext db)
        {
            _db = db;
        }

        public ICollection<RoleDTO> GetAll()
        {
            return _db.Roles.ProjectTo<RoleDTO>().ToList();
        }

        public RoleDTO GetById(int id)
        {
            return _db.Roles.Where(r => r.Id == id).ProjectTo<RoleDTO>().FirstOrDefault();
        }
    }
}
