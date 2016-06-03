using System.Collections.Generic;

namespace CarLookUp.Data.Entities
{
    public class BodyType
    {
        public virtual ICollection<Car> Cars { get; set; }
        public int Id { get; set; }
        public string TypeOfBody { get; set; }
    }
}
