using CarLookUp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLookUp.Services.Interfaces
{
    /// <summary>
    /// Api to work with Role service
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// Gets all roles.
        /// </summary>
        /// <returns>ICollection</returns>
        ICollection<RoleDTO> GetAll();

        /// <summary>
        /// Gets the role by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>RoleDTO</returns>
        RoleDTO GetById(int id);
    }
}
