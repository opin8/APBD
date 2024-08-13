using LegacyApp;
using Xunit;

namespace LegacyAppTests;

public class UserServiceTests
{
    [Fact]
    public void AddUser_Should_Return_False_When_Missing_FirstName()
        {
            var service = new UserService();

            var result = service.AddUser(null, null, "kowalski@wp.pl", new DateTime(1980, 8, 18), 1);

            Assert.Equal(false,result);
        }

    [Fact]
    public void AddUser_Should_Return_False_When_Missing_At_Sign_And_Dot_In_Email()
    {
        
        var service = new UserService();
        
        var result = service.AddUser("John", "Kowalski", "kowalskiwppl", new DateTime(1980, 8, 18), 1);
        
        Assert.Equal(false,result);

    }
    
    [Fact]
    public void AddUser_Should_Return_False_When_Younger_Than_21_Years_Old()
    {
        
        var service = new UserService();
        
        var result = service.AddUser("John", "Kowalski", "kowalski.wp.pl", new DateTime(2004, 8, 18), 1);
        
        Assert.Equal(false,result);

    }
    
    [Fact]
    public void AddUser_Should_Return_True_When_Very_Important_Client()
    {
        
        var service = new UserService();
        
        var result = service.AddUser("John", "Malewski", "kowalski.wp.pl", new DateTime(1980, 8, 18), 1);
        
        Assert.Equal(true,result);

    }
    
    [Fact]
    public void AddUser_Should_Return_True_When_Important_Client()
    {
        
        var service = new UserService();
        
        var result = service.AddUser("John", "Smith", "kowalski.wp.pl", new DateTime(1980, 8, 18), 1);
        
        Assert.Equal(true,result);

    }
    
    [Fact]
    public void AddUser_Should_Return_True_When_Normal_Client()
    {
        
        var service = new UserService();
        
        var result = service.AddUser("John", "Kwiatkowski", "kowalski.wp.pl", new DateTime(1980, 8, 18), 1);
        
        Assert.Equal(true,result);

    }
    
    [Fact]
    public void AddUser_Should_Return_False_When_Normal_Client_With_No_Credit_Limit()
    {
        
        var service = new UserService();
        
        var result = service.AddUser("John", "Kowalski", "kowalski.wp.pl", new DateTime(1980, 8, 18), 1);
        
        Assert.Equal(false,result);

    }
    
    /*[Fact]
    public void AddUser_Should_Throw_Exception_When_User_Does_Not_Exist()
    {
        
        var service = new UserService();
        
        var result = service.AddUser("John", "Kowalski", "kowalski.wp.pl", new DateTime(1980, 8, 18), 1);
        
        Assert.Equal(false,result);

    }*/
    
}