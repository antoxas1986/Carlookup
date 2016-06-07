using CarLookUp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLookUp.Data.Repository.Interfaces
{
    public interface IRoleRepo
    {
        ICollection<RoleDTO> GetAll();

        RoleDTO GetById(int id);
    }
}
