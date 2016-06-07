namespace CarLookUp.Core.Models
{
    /// <summary>
    /// Base carDTO model
    /// </summary>
    public class CarDTO
    {
        public int Id { get; set; }

        public string Maker { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }
    }
}
