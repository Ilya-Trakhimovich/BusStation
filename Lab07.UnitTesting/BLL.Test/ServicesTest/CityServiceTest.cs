using AutoMapper;
using Lab06.BL.DTO.CityDTO;
using Lab06.BL.Services.Concrete;
using Lab06.DAL.Entities;
using Lab06.DAL.UOW.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using MapProfile = Lab06.BL.Mapping.Mapper;

namespace Lab07.UnitTesting.BLL.Test.ServicesTest
{
    public class CityServiceTest
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly MapperConfiguration _mapperMock;
        private readonly IMapper _mapper;
        private readonly List<City> _emptyCityList;
        private readonly List<City> _cityList;

        public CityServiceTest()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _mapperMock = new MapperConfiguration(cfg => cfg.AddProfile(new MapProfile()));
            _mapper = _mapperMock.CreateMapper();
            _emptyCityList = new List<City>();

            _cityList = new List<City>
            {
                new City { Id = 1, Name = "1"},
                new City { Id = 2, Name = "2"},
                new City { Id = 3, Name = "3"}
            };
        }

        [Fact]
        public void GetAll_DbIsEmpty_ReturnEmptyCityList()
        {
            _uowMock.Setup(u => u.GetRepository<City>().GetAll()).Returns(_emptyCityList.AsQueryable());

            var cityService = new CityService(_uowMock.Object, _mapper);
            var result = cityService.GetAll();

            Assert.NotNull(result);
            Assert.IsType<List<CityDto>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public void GetAll_DbHasItems_ReturnCityDtoList()
        {
            _uowMock.Setup(u => u.GetRepository<City>().GetAll()).Returns(_cityList.AsQueryable());

            var cityService = new CityService(_uowMock.Object, _mapper);
            var result = cityService.GetAll();

            Assert.NotNull(result);
            Assert.IsType<List<CityDto>>(result);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public void AddItem_InvokeUowSaveAtOnce()
        {
            var mock = _uowMock.Setup(u => u.GetRepository<City>().Create(It.IsAny<City>()));
            var cityService = new CityService(_uowMock.Object, _mapper);

            var cityDto = new AddCityDto { Name = "Minsk" };
            cityService.AddItem(cityDto);

            _uowMock.Verify(u => u.Save(), Times.Once);
        }

        [Fact]
        public void GetCitiesNames_DbIsEmpty_ReturnEmptyNameList()
        {
            _uowMock.Setup(u => u.GetRepository<City>().GetAll()).Returns(_emptyCityList.AsQueryable());

            var cityService = new CityService(_uowMock.Object, _mapper);
            var result = cityService.GetCitiesNames();

            Assert.NotNull(result);
            Assert.IsType<List<string>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public void GetCitiesNames_DbHasCities_ReturnNameList()
        {
            _uowMock.Setup(u => u.GetRepository<City>().GetAll()).Returns(_cityList.AsQueryable());

            var cityService = new CityService(_uowMock.Object, _mapper);
            var result = cityService.GetCitiesNames();
            var expected = new List<string> { "1", "2", "3" };

            Assert.NotNull(result);
            Assert.IsType<List<string>>(result);
            Assert.Equal(expected, result);
        }
    }
}
