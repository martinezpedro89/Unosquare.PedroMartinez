namespace Unosquare.PedroMartinez.Prefilter.Data.Entities
{
    public class RealEstateProperty : IRealEstateProperty
    {
        public int ID { get; set; }
        public int Price { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
