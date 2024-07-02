namespace Unosquare.PedroMartinez.Prefilter.Data.Entities
{
    public interface IRealEstateProperty
    {
        public int ID { get; set; }
        public int Price { get; set; }
        public string Location { get; set; } 
        public string Description { get; set; } 
    }
}
