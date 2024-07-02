using AutoFixture;
using FluentAssertions;
using Unosquare.PedroMartinez.Prefilter.Data.Entities;
using Unosquare.PedroMartinez.Prefilter.Data.Repositories;
using Unosquare.PedroMartinez.Prefilter.Exceptions;

namespace Unosquare.PedroMartinez.Prefilter.Tests
{
    [TestFixture]
    public class PropertyRepositoryTests
    {
        private Fixture fixture;
        private PropertyRepository repository;

        [SetUp]
        public void Setup()
        {
            fixture = new Fixture();
            repository = new PropertyRepository();
        }

        [Test]
        public async Task Add_ValidProperty_ReturnsTrue()
        {
            // Arrange
            var newProperty = fixture.Create<RealEstateProperty>();

            // Act
            var result = await repository.Add(newProperty);

            // Assert
            result.Should().BeTrue();
            repository.GetAll().Result.Should().ContainEquivalentOf(newProperty);
        }

        [Test]
        public async Task GetAll_WhenListOfPropertiesIsNull_ReturnsEmptyList()
        {
            // Act
            var result = await repository.GetAll();

            // Assert
            result.Should().BeEmpty();
        }

        [Test]
        public async Task GetByLocation_WhenLocationMatches_ReturnsMatchingProperties()
        {
            // Arrange
            var location = "New York";
            var property1 = fixture.Build<RealEstateProperty>().With(p => p.Location, location).Create();
            var property2 = fixture.Build<RealEstateProperty>().With(p => p.Location, "California").Create();
            repository.Add(property1).Wait();
            repository.Add(property2).Wait();

            // Act
            var result = await repository.GetByLocation(location);

            // Assert
            result.Should().ContainEquivalentOf(property1);
            result.Should().NotContain(property2);
        }

        [Test]
        public async Task GetByPrice_WhenPriceRangeMatches_ReturnsMatchingProperties()
        {
            // Arrange
            var minPrice = 100000;
            var maxPrice = 200000;
            var property1 = fixture.Build<RealEstateProperty>().With(p => p.Price, 150000).Create();
            var property2 = fixture.Build<RealEstateProperty>().With(p => p.Price, 250000).Create();
            repository.Add(property1).Wait();
            repository.Add(property2).Wait();

            // Act
            var result = await repository.GetByPrice(minPrice, maxPrice);

            // Assert
            result.Should().ContainEquivalentOf(property1);
            result.Should().NotContain(property2);
        }

        [Test]
        public async Task Remove_ExistingProperty_RemovesFromList()
        {
            // Arrange
            var propertyToRemove = fixture.Create<RealEstateProperty>();
            repository.Add(propertyToRemove).Wait();

            // Act
            await repository.Remove(propertyToRemove.ID);
            var allProperties = await repository.GetAll();

            // Assert
            allProperties.Should().NotContain(propertyToRemove);
        }

        [Test]
        public async Task Update_ExistingProperty_UpdatesProperties()
        {
            // Arrange
            var existingProperty = fixture.Create<RealEstateProperty>();
            repository.Add(existingProperty).Wait();
            var updatedProperty = new RealEstateProperty
            {
                ID = existingProperty.ID,
                Location = "Updated Location",
                Price = existingProperty.Price + 10000,
                Description = "Updated Description"
            };

            // Act
            var result = await repository.Update(updatedProperty);

            // Assert
            result.Should().NotBeNull();
            result.Location.Should().Be(updatedProperty.Location);
            result.Price.Should().Be(updatedProperty.Price);
            result.Description.Should().Be(updatedProperty.Description);
        }

        [Test]
        public void Update_NonExistingProperty_ThrowsPropertyNotFoundException()
        {
            // Arrange
            var nonExistingProperty = fixture.Create<RealEstateProperty>();

            // Act
            Func<Task> act = async () => await repository.Update(nonExistingProperty);

            // Assert
            act.Should().ThrowAsync<PropertyNotFoundException>();
        }
    }
}