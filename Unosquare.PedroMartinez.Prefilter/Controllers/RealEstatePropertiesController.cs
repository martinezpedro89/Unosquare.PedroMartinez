using Microsoft.AspNetCore.Mvc;
using Unosquare.PedroMartinez.Prefilter.Data.Entities;
using Unosquare.PedroMartinez.Prefilter.Data.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Unosquare.PedroMartinez.Prefilter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RealEstatePropertiesController : ControllerBase
    {
        private readonly IPropertyRepository _propertyRepository;

        public RealEstatePropertiesController(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        // GET: api/<RealEstatePropertiesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _propertyRepository.GetAll());
        }

        // GET api/<RealEstatePropertiesController>/nm
        [HttpGet("{location}")]
        public async Task<IActionResult> GetByLocation(string location)
        {
            var result = await _propertyRepository.GetByLocation(location);
            if (result != null && result.Count > 0)
            {
                return Ok(result);
            }
            else
            {
                return NotFound("No properties were found for that location");
            }            
        }

        // GET api/<RealEstatePropertiesController>/nm
        [HttpGet("{minPrice}, {maxPrice}")]
        public async Task<IActionResult> GetByPrice(int minPrice, int maxPrice)
        {
            var result = await _propertyRepository.GetByPrice(minPrice, maxPrice);
            if (result != null && result.Count > 0)
            {
                return Ok(result);
            }
            else
            {
                return NotFound("No properties were found for that price range.");
            }
        }

        // POST api/<RealEstatePropertiesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RealEstateProperty newProperty)
        {
            return Ok(await _propertyRepository.Add(newProperty));
        }

        // PUT api/<RealEstatePropertiesController>/5
        [HttpPatch]
        public async Task<IActionResult> Put([FromBody] RealEstateProperty newProperty)
        {
            return Ok(await _propertyRepository.Update(newProperty));
        }

        // DELETE api/<RealEstatePropertiesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _propertyRepository.Remove(id));
        }
    }
}
