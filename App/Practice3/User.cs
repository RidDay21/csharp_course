using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace App.Practice3;

public class User
{
    public string Name { get; init; }
    public string Surname { get; init; }
    public string Inn { get; init; }
    public string Login { get; init; }
    public string PasswordHash { get; init; }
    public Guid ID { get; init; }
    public DateTime RegisterTime { get; init; }

    private string phone;
    public string Phone
    {
        get => phone;
        set => phone = TryUpdatePhone(value);
    }
    


    public User(string login, string hashPassword, string name, string surname, string inn, string phone)
    {
        Login = login;
        Name = name;
        Surname = surname;
        Inn = inn;
        Phone = phone;
        PasswordHash = hashPassword;
        ID = Guid.NewGuid();
        RegisterTime = DateTime.Now;
    }

    public User()
    {
        ID = Guid.NewGuid();
        RegisterTime = DateTime.Now;
    }

    public string GetUserFullName()
    {
        return $"User: {Name} {Surname}";
    }

    static bool IsPhoneValid(string inputString, out string parsedPhone)
    {
        parsedPhone = "";
        var pattern = @"\b(\+?[87])([-\s]*\(?\d{3}\)?[-\s]*\d{3}[-\s]*\d{2}[-\s]*\d{2})\b";
        var regex = new Regex(pattern);
        var matches = regex.Matches(inputString);
        var counter = matches.Count();
        if (counter != 0)
        {
            parsedPhone = matches[0].Value;
            return true;
        }

        Console.WriteLine("Данный телефонный номер невалиден или написан в неподдерживаемом формате ");
        return false;
    }

    private string TryUpdatePhone(string newPhone)
    {
        if (IsPhoneValid(newPhone, out var tmpPhone))
        {
            Console.WriteLine($"Телефон успешно изменен. Новый номер: {phone}");
            return tmpPhone;
        }
 
            Console.WriteLine("Телефонный номер не валиден");
            return null;
    }
}