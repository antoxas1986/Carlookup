using CarLookUp.Data.Entities;
using System;
using System.Data.Entity;

namespace CarLookUp.Data.Context.Interfaces
{
    public interface ICarContext : IDisposable
    {
        DbSet<BodyType> BodyTypes { get; set; }
        DbSet<Car> Cars { get; set; }

        int SaveChanges();
    }
}
