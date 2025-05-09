using App.Practice3;

namespace App;

public static class Program
{
    public static void Main()
    {
        var user1 = new User("Rid21", "0487", "Nikolay", "Laptev", "7830002293", "8-913-777-77-77");
        var user2 = new User ("GRUSHA", "mcl4r3n", "Grigoriy", "Kozlov", "7703399790", "+7(934) 785-23-12");
        Console.WriteLine(user1.GetUserFullName());
        Console.WriteLine(user2.GetUserFullName());
        
        //string invalidPhoneNumber = "7913 777 77 777";
        var validPhoneNumber = "+7 (913) 876 54 32";

        var user3 = UserCreator.CreateUser(
            "boris",
            "12d33f",
            "kirill",
            "kochanov",
            "7703399790",
            validPhoneNumber);
        Console.WriteLine(user3.PasswordHash);
        
        user3.Phone = "+8-913-780-04-32";
        Console.WriteLine(user3.GetUserFullName());
        
        

    }
}