using CarLookUp.Data.Entities;
using System.Collections.Generic;
using CarLookUp.Core.Models;

namespace CarLookUp.Data.Repository.Interfaces
{
    public interface IBodyTypeRepository
    {
        ICollection<T> GetAll<T>();
        BodyTypeDTO GetById(int id);
    }
}
