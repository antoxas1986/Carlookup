using CarLookUp.Data.Entities;
using System.Collections.Generic;

namespace CarLookUp.Data.Repository.Interfaces
{
    public interface IBodyTypeRepository
    {
        ICollection<T> GetAll<T>();
    }
}
