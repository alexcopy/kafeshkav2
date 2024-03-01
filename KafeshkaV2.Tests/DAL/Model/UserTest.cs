using JetBrains.Annotations;
using KafeshkaV2.DAL.Model;
using Xunit;

namespace KafeshkaV2.Tests.DAL.Model;

[TestSubject(typeof(User))]
public class UserTest
{

    [Fact]
    public void User_PropertiesShouldBeMappedCorrectly()
    {
        // Arrange
        var user = new User
        {
            UserId = 1,
            email = "test@example.com",
            password = "securepassword",
            FirstName = "John",
            LastName = "Doe"
        };
        
 
        Assert.Equal(1, user.UserId);
        Assert.Equal("test@example.com", user.email);
        Assert.Equal("securepassword", user.password);
        Assert.Equal("John", user.FirstName);
        Assert.Equal("Doe", user.LastName);
    }
}