using System.Collections.Generic;

namespace CarLookUp.Core.Models
{
    public class BodyTypeDTO
    {
        public virtual ICollection<CarDTO> Cars { get; set; }
        public int Id { get; set; }
        public string TypeOfBody { get; set; }
    }
}
