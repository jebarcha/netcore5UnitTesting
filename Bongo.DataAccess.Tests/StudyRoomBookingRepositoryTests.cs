using Bongo.DataAccess.Repository;
using Bongo.Models.Model;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bongo.DataAccess.Tests
{
    [TestFixture]
    public class StudyRoomBookingRepositoryTests
    {
        private StudyRoomBooking _studyRoomBooking_One;
        private StudyRoomBooking _studyRoomBooking_Two;
        private DbContextOptions<ApplicationDbContext> _options;

        public StudyRoomBookingRepositoryTests()
        {
            _studyRoomBooking_One = new StudyRoomBooking()
            {
                FirstName = "Jesus1",
                LastName = "Barajas1",
                Date = new DateTime(2023, 1, 1),
                Email = "jesus1@demo.com",
                BookingId = 11,
                StudyRoomId = 1
            };

            _studyRoomBooking_Two = new StudyRoomBooking()
            {
                FirstName = "Jesus2",
                LastName = "Barajas2",
                Date = new DateTime(2023, 2, 2),
                Email = "jesus2@demo.com",
                BookingId = 22,
                StudyRoomId = 2
            };
        }
        
        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "temp_Bongo").Options;

        }

        [Test]
        [Order(1)]
        public void SaveBooking_Booking_One_CheckTheValuesFromDatabase()
        {
            //Arrange
            
            //Act
            using (var context = new ApplicationDbContext(_options))
            {
                var repo = new StudyRoomBookingRepository(context);
                repo.Book(_studyRoomBooking_One);
            }

            //Assert
            using (var context = new ApplicationDbContext(_options))
            {
                var bookingFromDb = context.StudyRoomBookings.FirstOrDefault(u=>u.BookingId==11);
                Assert.AreEqual(_studyRoomBooking_One.BookingId, bookingFromDb.BookingId);
                Assert.AreEqual(_studyRoomBooking_One.FirstName, bookingFromDb.FirstName);
                Assert.AreEqual(_studyRoomBooking_One.LastName, bookingFromDb.LastName);
                Assert.AreEqual(_studyRoomBooking_One.Email, bookingFromDb.Email);
                Assert.AreEqual(_studyRoomBooking_One.Date, bookingFromDb.Date);
            }
        }

        [Test]
        [Order(2)]
        public void GetAllBooking_BookingOneAndTwo_CheckBothTheBookingFromDatabase()
        {
            //Arrange
            var expectedResult = new List<StudyRoomBooking> { _studyRoomBooking_One, _studyRoomBooking_Two };

            using (var context = new ApplicationDbContext(_options))
            {
                context.Database.EnsureDeleted();
                var repo = new StudyRoomBookingRepository(context);
                repo.Book(_studyRoomBooking_One);
                repo.Book(_studyRoomBooking_Two);
            }

            //Act
            List<StudyRoomBooking> actualList;
            using (var context = new ApplicationDbContext(_options))
            {
                var repo = new StudyRoomBookingRepository(context);
                actualList = repo.GetAll(null).ToList();
            }

            //Assert
            CollectionAssert.AreEqual(expectedResult, actualList, new BookingCompare());
        }
        private class BookingCompare : IComparer
        {
            public int Compare(object x, object y)
            {
                var booking1 = (StudyRoomBooking)x;
                var booking2 = (StudyRoomBooking)y;
                if (booking1.BookingId != booking2.BookingId)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
