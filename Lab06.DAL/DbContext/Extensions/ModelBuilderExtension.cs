using Lab06.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Lab06.DAL.DbContext.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var userId = Guid.NewGuid().ToString();
            var userId2 = Guid.NewGuid().ToString();
            var roleIdCustomer = Guid.NewGuid().ToString();
            var roleIdAdmin = Guid.NewGuid().ToString();

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole[]
            {
                new IdentityRole { Id = roleIdAdmin, Name = "admin", NormalizedName = "admin" } ,
                new IdentityRole{ Id = roleIdCustomer, Name = "customer", NormalizedName = "customer"}
            });

            var hash = new PasswordHasher<ApplicationUser>();

            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser[]
                {
                    new ApplicationUser {  Id = userId,  UserName = "admin", NormalizedUserName = "ADMIN", PasswordHash = hash.HashPassword(null, "qwerty")},
                    new ApplicationUser{ Id = userId2, UserName = "ponny", NormalizedUserName = "PONNY", PasswordHash = hash.HashPassword(null, "qwerty")}
                }
            );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>[]
                    {
                     new IdentityUserRole<string>{  RoleId = roleIdAdmin, UserId = userId},
                     new IdentityUserRole<string>{  RoleId = roleIdCustomer, UserId = userId2}
                    }
            );

            modelBuilder.Entity<Bus>().HasData(
                    new Bus[]
                    {
                        new Bus{  Id = 1, SeatsCount = 20, RegisterNumber = "1234ph1"},
                        new Bus{  Id = 2, SeatsCount = 20, RegisterNumber = "2222mi2"},
                        new Bus{  Id = 3, SeatsCount = 20, RegisterNumber = "3212mi5"},
                        new Bus{  Id = 4, SeatsCount = 20, RegisterNumber = "7874mi5"},
                        new Bus{  Id = 5, SeatsCount = 20, RegisterNumber = "8453mi4"}
                    }
                );

            modelBuilder.Entity<City>().HasData(
                new City[]
                    {
                        new City{ Id = 1, Name = "Minsk"},
                        new City{ Id = 2, Name = "Grodno"},
                        new City{ Id = 3 , Name = "Molodechno"},
                        new City{ Id = 4, Name = "Orsha"},
                        new City{ Id = 5, Name = "Pinsk"},
                        new City{ Id = 6, Name = "Borisov"},
                        new City{ Id = 7, Name = "Mogilev"}
                    }
                );

            modelBuilder.Entity<Trip>().HasData(
                new Trip[]
                    {
                        new Trip
                        {
                            Id = 1,
                            Cost = 11,
                            StartDate = new DateTime(2021, 4, 15),
                            FinishDate = new DateTime(2021, 4, 15),
                            StartTrip = new TimeSpan(6,0,0),
                            FinishTip = new TimeSpan(6,50,0),
                            BusId = 1,
                            CityId = 2,
                            FreeSeats = 14,
                            CountSoldSeats = 6,
                            IsCanceled = false
                        },
                        new Trip
                        {
                            Id = 2,
                            Cost = 10,
                            StartDate = new DateTime(2021, 4, 15),
                            FinishDate = new DateTime(2021, 4, 15),
                            StartTrip = new TimeSpan(7,0,0),
                            FinishTip = new TimeSpan(7,30,0),
                            BusId = 2,
                            CityId = 3,
                            FreeSeats = 13,
                            CountSoldSeats = 7,
                            IsCanceled = false
                        },
                        new Trip
                        {
                            Id = 3,
                            Cost = 9,
                            StartDate = new DateTime(2021, 4, 15),
                            FinishDate = new DateTime(2021, 4, 15),
                            StartTrip = new TimeSpan(8,0,0),
                            FinishTip = new TimeSpan(9,35,0),
                            BusId = 3,
                            CityId = 3,
                            FreeSeats = 13,
                            CountSoldSeats = 7,
                            IsCanceled = false
                        },
                        new Trip
                        {
                            Id = 4,
                            Cost = 9,
                            StartDate = new DateTime(2021, 3, 15),
                            FinishDate = new DateTime(2021, 3, 15),
                            StartTrip = new TimeSpan(22,0,0),
                            FinishTip = new TimeSpan(23,0,0),
                            BusId = 1,
                            CityId = 1,
                            FreeSeats = 11,
                            CountSoldSeats = 9,
                            IsCanceled = false
                        },
                        new Trip
                        {
                            Id = 5,
                            Cost = 11,
                            StartDate = new DateTime(2021, 2, 15),
                            FinishDate = new DateTime(2021, 2, 15),
                            StartTrip = new TimeSpan(11,0,0),
                            FinishTip = new TimeSpan(12,50,0),
                            BusId = 3,
                            CityId = 3,
                            FreeSeats = 12,
                            CountSoldSeats = 8,
                            IsCanceled = false
                        }
                     }
                );

            modelBuilder.Entity<Ticket>().HasData(
                new Ticket[]
                    {
                       new Ticket{ Id = 1, ApplicationUserId = userId2, TripId = 1, IsCanceled = false},
                       new Ticket{ Id =2, ApplicationUserId = userId2, TripId = 2, IsCanceled = false},
                       new Ticket{ Id = 3, ApplicationUserId = userId2, TripId = 3, IsCanceled = false},
                       new Ticket{ Id = 4, ApplicationUserId = userId2, TripId = 4, IsCanceled = false}
                    }
                );
        }
    }
}
