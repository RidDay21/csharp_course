using System.Security.Cryptography;
using System.Text;

namespace App.Practice3;

static class UserCreator
{
    public static User CreateUser(string login, string password, string name, string surname, string inn, string phone)
    {
        var hashedPassword = HashPassword(password);
        var user1 = new User(login, hashedPassword, name, surname, inn, phone);
        return user1;
    }

    private static string HashPassword(string password)
    {
        var MD5Hash = MD5.Create();
        var inputBytes = Encoding.ASCII.GetBytes(password);
        var hash = MD5Hash.ComputeHash(inputBytes);
        return Convert.ToHexString(hash);
    } 
    
    
     
}