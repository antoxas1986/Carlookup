using CarLookUp.Data.Context.Interfaces;
using CarLookUp.Data.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CarLookUp.Data.Context
{
    public class CarContext : DbContext, ICarContext
    {
        public CarContext() : base("CarContext")
        {
        }

        public DbSet<BodyType> BodyTypes { get; set; }

        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
