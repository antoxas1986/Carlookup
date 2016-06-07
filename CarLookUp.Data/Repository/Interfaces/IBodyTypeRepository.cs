using CarLookUp.Core.Models;
using CarLookUp.Data.Entities;
using System.Collections.Generic;

namespace CarLookUp.Data.Repository.Interfaces
{
    /// <summary>
    /// Api to work with BodyType model
    /// </summary>
    public interface IBodyTypeRepository
    {
        /// <summary>
        /// Gets all bodytypes model.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        ICollection<T> GetAll<T>();

        /// <summary>
        /// Gets the bodytype by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        BodyTypeDTO GetById(int id);
    }
}
