using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLookUp.Data.Repository.Interfaces
{
    /// <summary>
    /// Inteface for unitofwork
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Saves the changes in Entity Framework.
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
    }
}
