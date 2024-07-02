using Unosquare.PedroMartinez.Prefilter.Data.Entities;

namespace Unosquare.PedroMartinez.Prefilter.Data.Repositories
{
    public interface IPropertyRepository
    {
        public Task<bool> Add(IRealEstateProperty newProperty);
        public Task<List<IRealEstateProperty>> Remove(int id);
        public Task<List<IRealEstateProperty>> GetAll();
        public Task<List<IRealEstateProperty>> GetByPrice(int minPrice, int maxPrice);
        public Task<List<IRealEstateProperty>> GetByLocation(string location);
        public Task<IRealEstateProperty> Update (IRealEstateProperty newProperty);
    }
}
