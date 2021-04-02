using AutoMapper;
using Lab06.BL.DTO.TripDTO;
using Lab06.BL.Services.Concrete;
using Lab06.BL.Services.Interfaces;
using Lab06.DAL.Entities;
using Lab06.DAL.UOW.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using MapProfile = Lab06.BL.Mapping.Mapper;

namespace Lab07.UnitTesting.BLL.Test.ServicesTest
{
    public class TripServiceTest
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly MapperConfiguration _mapperMock;
        private readonly IMapper _mapper;
        private readonly Mock<IDatetimeService> _dtServiceMock;
        private readonly List<Trip> _actualTrips;
        private readonly Trip _trip;
        private readonly Trip _nullTrip;

        public TripServiceTest()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _mapperMock = new MapperConfiguration(cfg => cfg.AddProfile(new MapProfile()));
            _mapper = _mapperMock.CreateMapper();
            _dtServiceMock = new Mock<IDatetimeService>();
            _nullTrip = null; ;

            _actualTrips = new List<Trip> //dates have utc format
            {
                new Trip{ StartDate = new DateTime(2021, 1, 1), StartTrip = new TimeSpan(6, 00 , 00), CityId = 1},
                new Trip{ StartDate = new DateTime(2021, 1, 1), StartTrip = new TimeSpan(11, 00 , 00), CityId = 2},
                new Trip{ StartDate = new DateTime(2021, 2, 1), StartTrip = new TimeSpan(10, 00 , 00), CityId = 1},
                new Trip{ StartDate = new DateTime(2021, 3, 1), StartTrip = new TimeSpan(10, 00 , 00), CityId = 3},
                new Trip{ StartDate = new DateTime(2021, 4, 1), StartTrip = new TimeSpan(10, 00 , 00), CityId = 2},
                new Trip{ StartDate = new DateTime(2021, 5, 1), StartTrip = new TimeSpan(10, 00 , 00), CityId = 4},
                new Trip{ StartDate = new DateTime(2021, 5, 1), StartTrip = new TimeSpan(10, 00 , 00), CityId = 1}
            };

            _trip = new Trip
            {
                Id = 2,
                BusId = 3,
                CityId = 4,
                CountSoldSeats = 13,
                StartDate = new DateTime(2021, 2, 2),
                StartTrip = new TimeSpan(10, 00, 00),
                Tickets = new List<Ticket>
                {
                    new Ticket{ Id = 2, IsCanceled = false, TripId =2, Trip = new Trip{  FreeSeats = 10} },
                    new Ticket{ Id = 3, IsCanceled = false, TripId = 2, Trip = new Trip{ FreeSeats = 9} }
                }
            };
        }

        [Fact]
        public void GetActualDates_DbIsEmpty_ReturnEmptyDatetimeList()
        {
            _uowMock.Setup(u => u.GetRepository<Trip>().GetAll()).Returns(new List<Trip>().AsQueryable());
            _dtServiceMock.Setup(d => d.GetLocalDatetimeNow()).Returns(new DateTime(2021, 1, 1));

            var tripService = new TripService(_uowMock.Object, _mapper, _dtServiceMock.Object);
            var result = tripService.GetActualDates();

            Assert.NotNull(result);
            Assert.IsType<List<DateTime>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public void GetActualDates_DbHasDates_ReturnActualDistinctDatetimeList()
        {
            _uowMock.Setup(u => u.GetRepository<Trip>().GetAll()).Returns(_actualTrips.AsQueryable());
            _dtServiceMock.Setup(d => d.GetLocalDatetimeNow()).Returns(new DateTime(2021, 1, 1));

            var tripService = new TripService(_uowMock.Object, _mapper, _dtServiceMock.Object);
            var result = tripService.GetActualDates();

            Assert.NotNull(result);
            Assert.IsType<List<DateTime>>(result);
            Assert.Equal(5, result.Count);
            Assert.True(result.Count(x => x.Date == new DateTime(2021, 5, 1)) == 1);
        }

        [Fact]
        public void GetDateTrips_PastDatetime_ReturnEmptyTripDtoList()
        {
            _uowMock.Setup(u => u.GetRepository<Trip>());
            _dtServiceMock.Setup(d => d.GetLocalDatetimeNow()).Returns(new DateTime(2021, 1, 1));

            var tripService = new TripService(_uowMock.Object, _mapper, _dtServiceMock.Object);
            var result = tripService.GetDateTrips(It.IsAny<int>(), new DateTime(2020, 12, 31));

            Assert.NotNull(result);
            Assert.IsType<List<TripDto>>(result);
            Assert.Empty(result);
        }

        [Theory]
        [MemberData(nameof(GetDates))]
        public void GetDateTrips_ActualDatetime_ReturnTripDtoList(DateTime date, List<TripDto> expected) //date is local time
        {
            _uowMock.Setup(u => u.GetRepository<Trip>().GetAll()).Returns(_actualTrips.AsQueryable());
            _dtServiceMock.Setup(d => d.GetLocalDatetimeNow()).Returns(new DateTime(2021, 1, 1, 8, 00, 00));

            var tripService = new TripService(_uowMock.Object, _mapper, _dtServiceMock.Object);
            var result = tripService.GetDateTrips(expected.FirstOrDefault().CityId, date);

            Assert.NotNull(result);
            Assert.IsType<List<TripDto>>(result);
            Assert.Equal(expected.Count, result.Count);
            Assert.Equal(expected.FirstOrDefault().StartTrip, result.FirstOrDefault().StartTrip);
        }

        [Fact]
        public void GetAll_DbIsEmpty_ReturnEmptyTripDtoList()
        {
            _uowMock.Setup(u => u.GetRepository<Trip>().GetAll()).Returns(new List<Trip>().AsQueryable());

            var tripService = new TripService(_uowMock.Object, _mapper, _dtServiceMock.Object);
            var result = tripService.GetAll();

            Assert.NotNull(result);
            Assert.IsType<List<TripDto>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public void GetAll_DbHasTrips_ReturnTripDtoList()
        {
            _uowMock.Setup(u => u.GetRepository<Trip>().GetAll()).Returns(_actualTrips.AsQueryable());

            var tripService = new TripService(_uowMock.Object, _mapper, _dtServiceMock.Object);
            var result = tripService.GetAll();

            Assert.NotNull(result);
            Assert.IsType<List<TripDto>>(result);
            Assert.Equal(7, result.Count);
        }

        [Fact]
        public void AddTrip_InvokeUowSaveAtOnce()
        {
            _uowMock.Setup(u => u.GetRepository<Trip>().Create(It.IsAny<Trip>()));

            var tripService = new TripService(_uowMock.Object, _mapper, _dtServiceMock.Object);
            tripService.AddTrip(It.IsAny<TripDto>());

            _uowMock.Verify(u => u.Save(), Times.Once);
        }

        [Fact]
        public void UpdateItem_InvokeUowSaveAtOnce()
        {
            _uowMock.Setup(u => u.GetRepository<Trip>().Update(_trip));

            var tripService = new TripService(_uowMock.Object, _mapper, _dtServiceMock.Object);
            var tripDto = _mapper.Map<TripDto>(_trip);
            tripService.UpdateItem(tripDto);

            _uowMock.Verify(u => u.Save(), Times.Once);
            Assert.Equal(tripDto.Id, _trip.Id);
            Assert.Equal(tripDto.BusId, _trip.BusId);
            Assert.Equal(tripDto.CityId, _trip.CityId);
            Assert.Equal(tripDto.CountSoldSeats, _trip.CountSoldSeats);
        }

        [Fact]
        public void GetItemById_TripDoesntExist_ReturnEmptyTripDto()
        {
            _uowMock.Setup(u => u.GetRepository<Trip>().GetItemById(It.IsAny<int>())).Returns(_nullTrip);

            var tripService = new TripService(_uowMock.Object, _mapper, _dtServiceMock.Object);
            var result = tripService.GetItemById(It.IsAny<int>());

            Assert.NotNull(result);
            Assert.IsType<TripDto>(result);
            Assert.Equal(default, result.Id);
            Assert.Equal(default, result.BusId);
            Assert.Equal(default, result.CityId);
            Assert.Equal(default, result.Cost);
            Assert.Equal(default, result.CountSoldSeats);
            Assert.Equal(default, result.FreeSeats);
        }

        [Fact]
        public void GetItemById_TripExistsInDb_ReturnTripDto()
        {
            _uowMock.Setup(u => u.GetRepository<Trip>().GetItemById(_trip.Id)).Returns(_trip);

            var tripService = new TripService(_uowMock.Object, _mapper, _dtServiceMock.Object);
            var result = tripService.GetItemById(_trip.Id);
            var expecredLocalTime = (_trip.StartDate + _trip.StartTrip);

            Assert.NotNull(result);
            Assert.IsType<TripDto>(result);
            Assert.Equal(expecredLocalTime, (result.StartDate + result.StartTrip));
        }

        [Fact]
        public void GetListCityTripsCountPerMonths_DbIsEmpty_ReturnEmptyNumberTripsList()
        {
            _uowMock.Setup(u => u.GetRepository<Trip>().GetAll()).Returns(new List<Trip>().AsQueryable());

            var tripService = new TripService(_uowMock.Object, _mapper, _dtServiceMock.Object);
            var result = tripService.GetListCityTripsCountPerMonths();

            Assert.NotNull(result);
            Assert.IsType<List<List<int>>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public void GetListCityTripsCountPerMonths_DbHasTrips_ReturnNumberTripsListPerMonth()
        {
            _uowMock.Setup(u => u.GetRepository<Trip>().GetAll()).Returns(_actualTrips.AsQueryable());

            var tripService = new TripService(_uowMock.Object, _mapper, _dtServiceMock.Object);
            var result = tripService.GetListCityTripsCountPerMonths();
            var groupCountExpected = _actualTrips.GroupBy(t => t.CityId).ToList();

            Assert.NotNull(result);
            Assert.IsType<List<List<int>>>(result);
            Assert.Equal(groupCountExpected.Count, result.Count);
        }

        [Fact]
        public void GetAverageCityTripCostPerMonth_DbIsEmpty_ReturnEmptyDoubleList()
        {
            _uowMock.Setup(u => u.GetRepository<Trip>().GetAll()).Returns(new List<Trip>().AsQueryable());

            var tripService = new TripService(_uowMock.Object, _mapper, _dtServiceMock.Object);
            var result = tripService.GetAverageCityTripCostPerMonth();

            Assert.NotNull(result);
            Assert.IsType<List<List<double>>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public void GetAverageCityTripCostPerMonth_DbHasTrips_ReturnAverageTripCostListPerMonth()
        {
            _uowMock.Setup(u => u.GetRepository<Trip>().GetAll()).Returns(_actualTrips.AsQueryable());

            var tripService = new TripService(_uowMock.Object, _mapper, _dtServiceMock.Object);
            var result = tripService.GetAverageCityTripCostPerMonth();
            var groupCountExpected = _actualTrips.GroupBy(t => t.CityId).ToList();
            var averageCostPerMonth = groupCountExpected.First().Average(t => t?.Cost ?? 0);

            Assert.NotNull(result);
            Assert.IsType<List<List<double>>>(result);
            Assert.Equal(groupCountExpected.Count, result.Count);
            Assert.Equal(averageCostPerMonth, result.First()[0]);
        }

        [Fact]
        public void CancelTrip_InvokeUowSaveAtOnce()
        {
            var tripMock = _uowMock.Setup(u => u.GetRepository<Trip>().GetItemById(_trip.Id)).Returns(_trip);
            var ticketsMock = _uowMock.Setup(u => u.GetRepository<Ticket>().GetAll()).Returns(_trip.Tickets.AsQueryable());

            var tripService = new TripService(_uowMock.Object, _mapper, _dtServiceMock.Object);
            tripService.CancelTrip(_trip.Id);

            _uowMock.Verify(u => u.Save(), Times.Once);
        }

        public static IEnumerable<object[]> GetDates()
        {
            var dates = new List<object[]>
            {
                new object[] { new DateTime(2021,1,1,10,45,00),
                               new List<TripDto> { new TripDto {CityId = 1,
                                                                StartDate = new DateTime(2021,1,1),
                                                                StartTrip = new TimeSpan(9,0,00)}, }},
                new object[] { new DateTime(2021,2,1,15,00,00),
                               new List<TripDto> { new TripDto {CityId = 1,
                                                                StartDate = new DateTime(2021,2,1),
                                                                StartTrip = new TimeSpan(13,0,00)} }}
            };

            return dates;
        }
    }
}