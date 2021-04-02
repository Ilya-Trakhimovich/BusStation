using AutoMapper;
using Lab06.BL.DTO.TicketDTO;
using Lab06.BL.ResultEnum;
using Lab06.BL.Services.Concrete;
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
    public class TicketServiceTest
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly MapperConfiguration _mapperMock;
        private readonly IMapper _mapper;
        private readonly int _ticketId;
        private readonly Guid _userId;
        private readonly Trip _nullTrip;
        private readonly Trip _trip;
        private readonly ApplicationUser _nullAppUser;
        private readonly ApplicationUser _appUser;
        private readonly List<Ticket> _emptyTicketList;
        private readonly List<Ticket> _ticketList;
        private readonly Ticket _ticket;
        private readonly Ticket _nullTicket;

        public TicketServiceTest()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _mapperMock = new MapperConfiguration(cfg => cfg.AddProfile(new MapProfile()));
            _mapper = _mapperMock.CreateMapper();
            _ticketId = 1;
            _userId = Guid.NewGuid();
            _nullTrip = null;
            _nullAppUser = null;
            _appUser = new ApplicationUser();
            _emptyTicketList = new List<Ticket>();
            _nullTicket = null;

            _ticket = new Ticket
            {
                Id = 1,
                IsCanceled = true,
                ApplicationUserId = "1",
                TripId = 2,
                Trip = new Trip
                {
                    FreeSeats = 10
                }
            };

            _trip = new Trip
            {
                FreeSeats = 10,
                CountSoldSeats = 10,
                Bus = new Bus
                {
                    SeatsCount = 20
                }
            };

            _ticketList = new List<Ticket>
            {
                 new Ticket { Id = 1},
                 new Ticket { Id = 2},
                 new Ticket { Id = 3}
            };
        }

        [Fact]
        public void BuyTicket_TripDoesntExist_ReturnFailResult()
        {
            _uowMock.Setup(u => u.GetRepository<Trip>().GetItemById(It.IsAny<int>())).Returns(_nullTrip);
            _uowMock.Setup(u => u.GetRepository<ApplicationUser>().GetItemById(_appUser.Id)).Returns(_appUser);

            var ticketService = new TicketService(_uowMock.Object, _mapper);
            var result = ticketService.BuyTicket(_ticketId, It.IsAny<Guid>());
            var expected = new BuyTicketOpertaionDto { Result = Result.Fail };

            Assert.NotNull(result);
            Assert.Equal(expected.Result, result.Result);
        }

        [Fact]
        public void BuyTicket_UserDoesntExist_ReturnFailResult()
        {
            _uowMock.Setup(u => u.GetRepository<Trip>().GetItemById(_trip.Id)).Returns(_trip);
            _uowMock.Setup(u => u.GetRepository<ApplicationUser>().GetItemById(It.IsAny<Guid>())).Returns(_nullAppUser);

            var ticketService = new TicketService(_uowMock.Object, _mapper);
            var result = ticketService.BuyTicket(It.IsAny<int>(), _userId);
            var expected = new BuyTicketOpertaionDto { Result = Result.Fail };

            Assert.NotNull(result);
            Assert.Equal(expected.Result, result.Result);
        }

        [Fact]
        public void GetAll_DbIsEmpty_ReturnEmptyTicketDtoList()
        {
            _uowMock.Setup(u => u.GetRepository<Ticket>().GetAll()).Returns(_emptyTicketList.AsQueryable());

            var ticketService = new TicketService(_uowMock.Object, _mapper);
            var result = ticketService.GetAll();

            Assert.NotNull(result);
            Assert.IsType<List<TicketDto>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public void GetAll_DbHasTickets_ReturnTicketDtoList()
        {
            _uowMock.Setup(u => u.GetRepository<Ticket>().GetAll()).Returns(_ticketList.AsQueryable());

            var ticketService = new TicketService(_uowMock.Object, _mapper);
            var result = ticketService.GetAll();

            Assert.NotNull(result);
            Assert.IsType<List<TicketDto>>(result);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public void GetItemById_TicketDoesntExist_ReturnEmptyTicketDto()
        {
            _uowMock.Setup(u => u.GetRepository<Ticket>().GetItemById(It.IsAny<int>())).Returns(_nullTicket);

            var ticketService = new TicketService(_uowMock.Object, _mapper);
            var result = ticketService.GetItemById(It.IsAny<int>());

            Assert.NotNull(result);
            Assert.IsType<TicketDto>(result);
            Assert.Equal(default, result.ApplicationUserId);
            Assert.Equal(default, result.Id);
            Assert.Equal(default, result.IsCanceled);
            Assert.Equal(default, result.Trip);
            Assert.Equal(default, result.TripId);
        }

        [Fact]
        public void GetItemById_TicketExistInDb_ReturnTicketDto()
        {
            _uowMock.Setup(u => u.GetRepository<Ticket>().GetItemById(_ticketId)).Returns(_ticket);

            var ticketService = new TicketService(_uowMock.Object, _mapper);
            var result = ticketService.GetItemById(_ticketId);
            var expected = _mapper.Map<TicketDto>(_ticket);

            Assert.NotNull(result);
            Assert.IsType<TicketDto>(result);
            Assert.Equal(expected.Id, result.Id);
            Assert.Equal(expected.ApplicationUserId, result.ApplicationUserId);
        }

        [Fact]
        public void UpdateItem_InvokeUowSaveAtOnce()
        {
            _uowMock.Setup(u => u.GetRepository<Ticket>().Update(It.IsAny<Ticket>())).Verifiable();

            var ticketService = new TicketService(_uowMock.Object, _mapper);
            ticketService.UpdateItem(It.IsAny<TicketDto>());

            _uowMock.Verify(u => u.Save(), Times.Once);
        }

        [Fact]
        public void CancelTicket_TicketExistsInDb_UpdateTicketProperties()
        {
            _uowMock.Setup(u => u.GetRepository<Ticket>().GetItemById(_ticketId)).Returns(_ticket);

            var ticketService = new TicketService(_uowMock.Object, _mapper);
            ticketService.CancelTicket(_ticketId);
            var expected = _mapper.Map<TicketDto>(_ticket);

            Assert.True(_ticket.IsCanceled);
            Assert.Equal(11, _ticket.Trip.FreeSeats);
            _uowMock.Verify(u => u.Save(), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetFakeTrips))]
        public void BuyTicket_DbHasTripAndUser_ReturnSuccessResult(Trip trip, BuyTicketOpertaionDto expected)
        {
            _uowMock.Setup(u => u.GetRepository<Trip>().GetItemById(trip.Id)).Returns(trip);
            _uowMock.Setup(u => u.GetRepository<ApplicationUser>().GetItemById(_appUser.Id)).Returns(_appUser);
            _uowMock.Setup(u => u.GetRepository<Ticket>().Create(It.IsAny<Ticket>()));

            var ticketService = new TicketService(_uowMock.Object, _mapper);
            var result = ticketService.BuyTicket(trip.Id, Guid.Parse(_appUser.Id));

            Assert.NotNull(result);
            Assert.Equal(expected.Result, result.Result);
        }

        public static IEnumerable<object[]> GetFakeTrips()
        {
            var trips = new List<object[]>
            {
                 new object[]
                 {
                     new Trip
                     {
                         Id= 1,
                         FreeSeats = 10,
                         CountSoldSeats = 10,
                         Bus = new Bus
                         {
                             SeatsCount = 20
                         }
                     },
                     new BuyTicketOpertaionDto
                     {
                         Result = Result.Success
                     }
                 },
                 new object[]
                 {
                     new Trip
                     {
                         Id =2,
                         FreeSeats = 0,
                         CountSoldSeats = 20,
                         Bus = new Bus
                         {
                             SeatsCount = 20
                         }
                     },
                     new BuyTicketOpertaionDto
                     {
                         Result = Result.Fail
                     } }
                 ,
                 new object[]
                 {
                     new Trip
                     {
                         Id =3,
                         FreeSeats = 20,
                         CountSoldSeats = 30,
                         Bus = new Bus
                         {
                             SeatsCount = 20
                         }
                     },
                     new BuyTicketOpertaionDto
                     {
                         Result = Result.Fail
                     }
                 }
            };

            return trips;
        }
    }
}