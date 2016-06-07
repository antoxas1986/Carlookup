namespace CarLookUp.Data.Migrations
{
    using Entities;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CarLookUp.Data.Context.CarContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CarLookUp.Data.Context.CarContext context)
        {
            var roles = new List<Role>()
            {
                new Role { Id=1, Name="User"},
                new Role { Id=2, Name="Admin" }
            };
            roles.ForEach(r => context.Roles.AddOrUpdate(p => p.Name, r));
            context.SaveChanges();

            var bodyTypes = new List<BodyType>()
            {
                new BodyType {Id = 1, TypeOfBody="Sedan" },
                new BodyType {Id=2, TypeOfBody="Coupe" },
                new BodyType {Id=3,TypeOfBody="Universal" },
                new BodyType {Id=4,TypeOfBody="Van" },
                new BodyType {Id=5,TypeOfBody="SUV" }
            };

            bodyTypes.ForEach(s => context.BodyTypes.AddOrUpdate(p => p.TypeOfBody, s));
            context.SaveChanges();

            var cars = new List<Car>() {
                new Car { Id = 1, Maker = "Ford", Model = "Escape", Year = 2015, BodyTypeId = bodyTypes.Single(b=>b.TypeOfBody=="Sedan").Id  },
                new Car { Id = 2, Maker = "Ford", Model = "Fusion", Year = 2016, BodyTypeId = bodyTypes.Single(b=>b.TypeOfBody=="Sedan").Id },
                new Car { Id = 3, Maker = "Chevy", Model = "Express", Year = 2006, BodyTypeId = bodyTypes.Single(b=>b.TypeOfBody=="Van").Id },
                new Car { Id = 4, Maker = "Toyota", Model = "Camry", Year = 2015, BodyTypeId = bodyTypes.Single(b=>b.TypeOfBody=="Sedan").Id },
                new Car { Id = 5, Maker = "BMW", Model = "E325", Year = 2008, BodyTypeId = bodyTypes.Single(b=>b.TypeOfBody=="Coupe").Id },
                new Car { Id = 6, Maker = "Audi", Model = "A4", Year = 2010, BodyTypeId = bodyTypes.Single(b=>b.TypeOfBody=="Sedan").Id },
                new Car { Id = 7, Maker = "Lexus", Model = "R350", Year = 2015, BodyTypeId = bodyTypes.Single(b=>b.TypeOfBody=="SUV").Id }
            };

            cars.ForEach(s => context.Cars.AddOrUpdate(p => p.Maker, s));
            context.SaveChanges();
        }
    }
}
