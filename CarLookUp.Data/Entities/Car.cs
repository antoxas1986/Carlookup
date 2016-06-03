namespace CarLookUp.Data.Entities
{
    public class Car
    {
        public virtual BodyType BodyType { get; set; }
        public int BodyTypeId { get; set; }
        public int Id { get; set; }

        public string Maker { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }
    }
}
