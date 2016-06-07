using CarLookUp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLookUp.Data.Repository.Interfaces
{
    /// <summary>
    /// Api to work with Role model
    /// </summary>
    public interface IRoleRepo
    {
        /// <summary>
        /// Gets all roles.
        /// </summary>
        /// <returns></returns>
        ICollection<RoleDTO> GetAll();

        /// <summary>
        /// Gets the role by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        RoleDTO GetById(int id);
    }
}
