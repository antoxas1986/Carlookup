namespace CarLookUp.Core.Models
{
    /// <summary>
    /// CarDto model with bodytype name and bodytype id
    /// </summary>
    /// <seealso cref="CarLookUp.Core.Models.CarDTO" />
    public class CarDTOWithBodyType : CarDTO
    {
        public string BodyType { get; set; }
        public int BodyTypeId { get; set; }
    }
}
