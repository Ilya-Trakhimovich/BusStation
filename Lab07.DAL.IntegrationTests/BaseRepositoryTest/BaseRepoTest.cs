using Lab06.DAL.DbContext;
using Lab06.DAL.Entities;
using Lab06.DAL.Repositories.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace Lab07.DAL.IntegrationTests.BaseRepositoryTest
{
    /// <summary>
    /// Test class works with a default seeded test db. Its not a real db and not real data
    /// Seed is in Lab06.DAL.DbContext.Extensions.ModelBuilderExtension
    /// </summary>
    public class BaseRepoTest
    {
        private BaseRepository<City> _cityRepo;
        private DbContextOptions<ApplicationContext> _options;

        [Fact]
        public void GetAll_ExecutedSuccessful()
        {
            _options = new DbContextOptionsBuilder<ApplicationContext>()
              .UseInMemoryDatabase("MemoryDbGetAll")
              .UseLazyLoadingProxies()
              .Options;

            using (var _context = new ApplicationContext(_options))
            {
                _context.Database.EnsureCreated();
                _cityRepo = new BaseRepository<City>(_context);

                var cities = _cityRepo.GetAll().ToList();
                var countCities = _cityRepo.GetAll().Count();
                var expected = 7; // 7 cities seeded by default 

                Assert.Equal(expected, countCities);
                Assert.Equal(4, cities[3].Id);
                Assert.Equal("Orsha", cities[3].Name);
                Assert.NotEmpty(cities[0].Trips); // Misk has trips by default
                Assert.Empty(cities[4].Trips); // Pinsk hasnt trips by default
            }
        }

        [Fact]
        public void Create_ExecutedSuccessful()
        {
            _options = new DbContextOptionsBuilder<ApplicationContext>()
                  .UseInMemoryDatabase("MemoryDbCreate")
                  .UseLazyLoadingProxies()
                  .Options;

            using (var _context = new ApplicationContext(_options))
            {
                _context.Database.EnsureCreated();
                _cityRepo = new BaseRepository<City>(_context);

                var countCitiesBefore = _cityRepo.GetAll().Count();
                var city = new City { Name = "Paris" };
                _cityRepo.Create(city);
                _context.SaveChanges();

                var citiesAfter = _cityRepo.GetAll();
                var lastAddedCity = citiesAfter.Last();

                Assert.Equal(countCitiesBefore, citiesAfter.Count() - 1);
                Assert.Equal(city.Name, lastAddedCity.Name);
            }
        }

        [Theory]
        [InlineData(-5)]
        [InlineData(int.MaxValue)]
        public void GetItemById_InvalidId_ReturnNull(int cityId)
        {
            _options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("MemoryDbGetItemByIdFail")
                .UseLazyLoadingProxies()
                .Options;

            using (var _context = new ApplicationContext(_options))
            {
                _context.Database.EnsureCreated();
                _cityRepo = new BaseRepository<City>(_context);

                var city = _cityRepo.GetItemById(cityId);

                Assert.Null(city);
            }
        }

        [Fact]
        public void GetItemById_ValidId_ReturnExiststingCity()
        {
            _options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("MemoryDbGetItemByIdSuccess")
                .UseLazyLoadingProxies()
                .Options;

            using (var _context = new ApplicationContext(_options))
            {
                _context.Database.EnsureCreated();
                _cityRepo = new BaseRepository<City>(_context);

                var existintCityId = 1;
                var city = _cityRepo.GetItemById(existintCityId);

                Assert.Equal(1, city.Id);
                Assert.Equal("Minsk", city.Name);
            }
        }

        [Fact]
        public void Update_ExecutedSuccessful()
        {
            _options = new DbContextOptionsBuilder<ApplicationContext>()
              .UseInMemoryDatabase("MemoryDbUpdate")
              .UseLazyLoadingProxies()
              .Options;

            using (var _context = new ApplicationContext(_options))
            {
                _context.Database.EnsureCreated();
                _cityRepo = new BaseRepository<City>(_context);

                var citiesBeforeUpdate = _cityRepo.GetAll().ToList();
                var updateCity = citiesBeforeUpdate.FirstOrDefault();

                updateCity.Name = "Test City";

                _cityRepo.Update(updateCity);
                _context.SaveChanges();

                var citiesAfterUpdate = _cityRepo.GetAll().ToList();

                Assert.Equal("Test City", citiesAfterUpdate.FirstOrDefault().Name);
            }
        }

        [Fact]
        public void Delete_ExecutedSuccessful()
        {
            _options = new DbContextOptionsBuilder<ApplicationContext>()
                 .UseInMemoryDatabase("MemoryDbDelete")
                 .UseLazyLoadingProxies()
                 .Options;

            using (var _context = new ApplicationContext(_options))
            {
                _context.Database.EnsureCreated();
                _cityRepo = new BaseRepository<City>(_context);

                var citiesBeforeDelete = _cityRepo.GetAll().ToList();
                var deleteCity = citiesBeforeDelete.FirstOrDefault();
                var countCitiesBefore = citiesBeforeDelete.Count();

                _cityRepo.Delete(deleteCity);
                _context.SaveChanges();

                var citiesAfterDelete = _cityRepo.GetAll().ToList();
                var countCitiesAfter = citiesAfterDelete.Count();

                Assert.Equal(countCitiesAfter, countCitiesBefore - 1);
                Assert.DoesNotContain(deleteCity, citiesAfterDelete);
            }
        }

        [Fact]
        public void Delete_InvalidArg_ThrowException()
        {
            _options = new DbContextOptionsBuilder<ApplicationContext>()
                 .UseInMemoryDatabase("MemoryDbDeleteException")
                 .UseLazyLoadingProxies()
                 .Options;

            using (var _context = new ApplicationContext(_options))
            {
                _context.Database.EnsureCreated();
                _cityRepo = new BaseRepository<City>(_context);

                Assert.Throws<DbUpdateConcurrencyException>(() =>
                {
                    _cityRepo.Delete(new City());
                    _context.SaveChanges();
                });
            }
        }
    }
}