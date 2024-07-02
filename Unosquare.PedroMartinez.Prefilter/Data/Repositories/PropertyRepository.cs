using Unosquare.PedroMartinez.Prefilter.Data.Entities;
using Unosquare.PedroMartinez.Prefilter.Exceptions;

namespace Unosquare.PedroMartinez.Prefilter.Data.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private List<IRealEstateProperty>? listOfProperties;

        public PropertyRepository()
        {
           listOfProperties = new List<IRealEstateProperty>();
        }

        public async Task<bool> Add(IRealEstateProperty newProperty)
        {
            try
            {
                if (listOfProperties == null)
                {
                    listOfProperties = new List<IRealEstateProperty>();
                    listOfProperties.Add(newProperty);
                }
                else
                {
                    listOfProperties.Add(newProperty);
                }
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                //log ex.message here.
                return await Task.FromResult(false);                
            }          
        }

        public async Task<List<IRealEstateProperty>> GetAll()
        {            
            if(listOfProperties == null)
            {
                return await Task.FromResult(new List<IRealEstateProperty>());
            }
            return await Task.FromResult(listOfProperties);
        }

        public async Task<List<IRealEstateProperty>> GetByLocation(string location)
        {
            try
            {
                if (listOfProperties == null)
                {
                    return await Task.FromResult(new List<IRealEstateProperty>());
                }
                var result = listOfProperties
                    .Where(property => property.Location.Equals(location, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                //log ex.message here.
                return await Task.FromResult(new List<IRealEstateProperty>());
            }
        }

        public async Task<List<IRealEstateProperty>> GetByPrice(int minPrice, int maxPrice)
        {
            try
            {
                if (listOfProperties == null)
                {
                    return await Task.FromResult(new List<IRealEstateProperty>());
                }
                var result = listOfProperties
                    .Where(property => property.Price >= minPrice && property.Price <= maxPrice)
                    .ToList();

                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                //log ex.message here.
                return await Task.FromResult(new List<IRealEstateProperty>());
            }
        }

        public async Task<List<IRealEstateProperty>> Remove(int id)
        {
            try
            {
                var propertyToRemove = listOfProperties!.FirstOrDefault(property => property.ID == id);
                if (propertyToRemove != null)
                {
                    listOfProperties!.Remove(propertyToRemove);
                }

                return await Task.FromResult(listOfProperties!);
            }
            catch (Exception ex)
            {
                //log ex.message here.
                return await Task.FromResult(new List<IRealEstateProperty>());
            }
        }         

        public async Task<IRealEstateProperty> Update(IRealEstateProperty newProperty)
        {
            try
            {
                var existingProperty = listOfProperties!.FirstOrDefault(property => property.ID == newProperty.ID);
                if (existingProperty != null)
                {
                    existingProperty.Location = newProperty.Location;
                    existingProperty.Price = newProperty.Price;
                    existingProperty.Description = newProperty.Description;
                    return await Task.FromResult(existingProperty);
                }
                else
                {
                    throw new PropertyNotFoundException($"Property with ID {newProperty.ID} not found.");
                }
            }
            catch (Exception ex)
            {
                //log ex.message here.
                throw;
            }
        }        
    }
}
