using AutoMapper;
using Lab06.BL.DTO.BusDTO;
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
    public class BusServiceTest
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly MapperConfiguration _mapperMock;
        private readonly IMapper _mapper;
        private readonly int _busId;
        private readonly Bus _bus;
        private readonly List<Bus> _emptyBusList;
        private readonly List<Bus> _busList;

        public BusServiceTest()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _mapperMock = new MapperConfiguration(cfg => cfg.AddProfile(new MapProfile()));
            _mapper = _mapperMock.CreateMapper();
            _emptyBusList = new List<Bus>();
            _busId = 1;

            _bus = new Bus
            {
                Id = 1,
                RegisterNumber = "123",
                SeatsCount = 20
            };

            _busList = new List<Bus>
            {
                 new Bus{  Id = 1},
                 new Bus{  Id = 2},
                 new Bus{  Id = 3}
            };
        }

        [Fact]
        public void GetItemById_BusDoestExistInDb_ReturnEmptyBusDto()
        {
            _uowMock.Setup(u => u.GetRepository<Bus>().GetItemById(_busId)).Returns(new Bus());

            var busService = new BusService(_uowMock.Object, _mapper);
            var result = busService.GetItemById(_busId);
            var expected = new BusDto();

            Assert.NotNull(result);
            Assert.Equal(expected.Id, result.Id);
            Assert.Equal(expected.RegisterNumber, result.RegisterNumber);
            Assert.Equal(expected.SeatsCount, result.SeatsCount);
        }

        [Fact]
        public void GetItemById_BusExistsInDb_ReturnBusDto()
        {
            _uowMock.Setup(u => u.GetRepository<Bus>().GetItemById(_busId)).Returns(_bus);

            var busService = new BusService(_uowMock.Object, _mapper);
            var result = busService.GetItemById(_busId);
            var expected = _mapper.Map<BusDto>(result);

            Assert.NotNull(result);
            Assert.IsType<BusDto>(result);
            Assert.Equal(expected.Id, result.Id);
            Assert.Equal(expected.RegisterNumber, result.RegisterNumber);
            Assert.Equal(expected.SeatsCount, result.SeatsCount);
        }

        [Fact]
        public void GetAll_DbIsEmpty_ReturnEmptyBusDtoList()
        {
            _uowMock.Setup(u => u.GetRepository<Bus>().GetAll()).Returns(_emptyBusList.AsQueryable());

            var busService = new BusService(_uowMock.Object, _mapper);
            var result = busService.GetAll();

            Assert.NotNull(result);
            Assert.IsType<List<BusDto>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public void GetAll_DbHasBuses_ReturnBusDtoList()
        {
            _uowMock.Setup(u => u.GetRepository<Bus>().GetAll()).Returns(_busList.AsQueryable());

            var busService = new BusService(_uowMock.Object, _mapper);
            var result = busService.GetAll();

            Assert.NotNull(result);
            Assert.IsType<List<BusDto>>(result);
            Assert.Equal(3, result.Count);
        }
    }
}