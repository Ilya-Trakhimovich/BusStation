using Lab06.BL.Services.Concrete;
using Lab06.DAL.Entities;
using Lab06.DAL.UOW.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Lab07.UnitTesting.BLL.Test.ServicesTest
{
    public class AppUserServiceTest
    {
        private readonly Guid _userId;
        private readonly ApplicationUser _appUserNull;
        private readonly ApplicationUser _appUserExist;

        public AppUserServiceTest()
        {
            _userId = Guid.NewGuid();
            _appUserNull = null;

            _appUserExist = new ApplicationUser
            {
                Id = _userId.ToString(),
                Tickets = new List<Ticket>
                {
                   new Ticket() { ApplicationUserId = _userId.ToString(), IsCanceled = false },
                   new Ticket() { ApplicationUserId = _userId.ToString(), IsCanceled = false },
                   new Ticket() { ApplicationUserId = _userId.ToString(), IsCanceled = false }
                }
            };
        }

        [Fact]
        public void GetCountActiveUSerTickets_UserDoesntExistInDb_ReturnZeroTickets()
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.GetRepository<ApplicationUser>().GetItemById(_userId)).Returns(_appUserNull);

            var appUserService = new AppUserService(mock.Object);
            var result = appUserService.GetCountActiveUSerTickets(_userId);

            var expected = 0;
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetCountActiveUSerTickets_UserExistInDb_ReturnNumberOfTickets()
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.GetRepository<ApplicationUser>().GetItemById(_appUserExist.Id)).Returns(_appUserExist);

            var appUserService = new AppUserService(mock.Object);
            var result = appUserService.GetCountActiveUSerTickets(Guid.Parse(_appUserExist.Id));

            var expected = 3;
            Assert.Equal(expected, result);
        }
    }
}