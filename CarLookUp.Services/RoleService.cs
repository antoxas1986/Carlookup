using CarLookUp.Core.Constants;
using CarLookUp.Core.Models;
using CarLookUp.Core.Utilities;
using CarLookUp.Data.Repository.Interfaces;
using CarLookUp.Services.Interfaces;
using System.Collections.Generic;

namespace CarLookUp.Services
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="CarLookUp.Services.Interfaces.IRoleService" />
    public class RoleService : IRoleService
    {
        private IRoleRepo _roleRepo;

        public RoleService(IRoleRepo roleRepo)
        {
            _roleRepo = roleRepo;
        }

        /// <summary>
        /// Gets all roles.
        /// </summary>
        /// <returns>
        /// ICollection
        /// </returns>
        public ICollection<RoleDTO> GetAll()
        {
            ICollection<RoleDTO> list = (ICollection<RoleDTO>)GlobalCachingProvider.Instance.GetItem(CacheKeys.ROLES);
            if (list == null)
            {
                list = _roleRepo.GetAll();
                GlobalCachingProvider.Instance.AddItem(CacheKeys.ROLES, list);
            }
            return list;
        }

        /// <summary>
        /// Gets the role by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// RoleDTO
        /// </returns>
        public RoleDTO GetById(int id)
        {
            RoleDTO role = (RoleDTO)GlobalCachingProvider.Instance.GetItem(CacheKeys.ROLES + id);
            if (role == null)
            {
                role = _roleRepo.GetById(id);
                if (role != null)
                {
                    GlobalCachingProvider.Instance.AddItem(CacheKeys.ROLES + id, role);
                }
            }
            return role;
        }
    }
}
