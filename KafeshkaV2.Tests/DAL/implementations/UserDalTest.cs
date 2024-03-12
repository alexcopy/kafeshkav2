using System;
using System.Linq;
using JetBrains.Annotations;
using KafeshkaV2.DAL.implementations;
using KafeshkaV2.DAL.Model;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace KafeshkaV2.Tests.DAL.implementations;

[TestSubject(typeof(UserDal))]
public class UserDalTest
{

 public class UserDalTests : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly DbContextOptions<AppDbContext> _options;

        public UserDalTests()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(_connection)
                .Options;

            using var dbContext = new AppDbContext(_options);
            dbContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _connection.Close();
        }

        [Fact]
        public void FindByEmail_Should_Return_Correct_User()
        {
            // Arrange
            using var dbContext = new AppDbContext(_options);
            var userDal = new UserDal(dbContext);

            var testEmail = "test@example.com";
            var testPassword = "passw0rd";
            var expectedUser = new User
            {
                UserId = 1,
                email = testEmail,
                FirstName = "John",
                LastName = "Doe",
                password = testPassword
            };
            dbContext.User.Add(expectedUser);
            dbContext.SaveChanges();

            // Act
            var result = userDal.FindByEmail(testEmail);
            var firstUser = result.FirstOrDefault();
            Assert.NotNull(firstUser);
            Assert.Equal(expectedUser.UserId, firstUser.UserId);
            Assert.Equal(expectedUser.email, firstUser.email);
        }

        [Fact]
        public void FindByEmail_Should_Return_Empty_Collection_If_User_Not_Found()
        {
            // Arrange
            using var dbContext = new AppDbContext(_options);
            var userDal = new UserDal(dbContext);

            var nonExistentEmail = "nonexistent@example.com";

            // Act
            var result = userDal.FindByEmail(nonExistentEmail);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void FindById_Should_Return_Correct_User()
        {
            // Arrange
            using var dbContext = new AppDbContext(_options);
            var userDal = new UserDal(dbContext);

            var testEmail = "test@example.com";
            var userId = 1;
            var testPassword = "passw0rd";
            var expectedUser = new User
            {
                UserId = userId,
                email = testEmail,
                FirstName = "John",
                LastName = "Doe",
                password = testPassword
            };

            dbContext.User.Add(expectedUser);
            dbContext.SaveChanges();

            // Act
            var result = userDal.FindById(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUser.UserId, result.UserId);
            Assert.Equal(expectedUser.email, result.email);
        }

        [Fact]
        public void FindById_Should_Return_Null_If_User_Not_Found()
        {
            // Arrange
            using var dbContext = new AppDbContext(_options);
            var userDal = new UserDal(dbContext);

            var nonExistentUserId = 999;

            // Act
            var result = userDal.FindById(nonExistentUserId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void AddUser_Should_Enforce_Unique_Email_Constraint()
        {
            // Arrange
            using var dbContext = new AppDbContext(_options);
            var userDal = new UserDal(dbContext);
            var testEmail = "test@example.com";
            var userId = 1;
            var testPassword = "passw0rd";
            var user1 = new User
            {
                UserId = userId,
                email = testEmail,
                FirstName = "John",
                LastName = "Doe",
                password = testPassword
            };

            // Act

            userDal.AddUser(user1);

            // Assert
            // Attempting to add a user with the same email should throw an exception
            Assert.Throws<DbUpdateException>(() =>
            {
                var user2 = new User { email = testEmail, password = "hashed_password_2" };
                userDal.AddUser(user2);
                dbContext.SaveChanges();
            });
        }
    }
}