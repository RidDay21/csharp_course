using System.Globalization;
using System.Text.RegularExpressions;

namespace App.Practice3;

public class User
{
    private Guid id;
    private string login;
    private string password;
    private readonly string  name;
    private readonly string surname;
    private readonly string inn;
    private string phone;
    private DateTime registerDate;

    public User(string login, string password, string name, string surname, string inn, string phone)
    {
        this.login = login;
        this.password = password;
        this.name = name;
        this.surname = surname;
        this.inn = inn;
        this.phone = phone;
    }

    public string GetUserFullName()
    {
        return $"User: {name} {surname}";
    }
    public static bool isPhoneValid(string inputString, out string parsedPhone) 
    {
        parsedPhone = "";
        string pattern = @"\b(\+?[87])([-\s]*\(?\d{3}\)?[-\s]*\d{3}[-\s]*\d{2}[-\s]*\d{2})\b";
        Regex regex = new Regex(pattern);
        MatchCollection matches  = regex.Matches(inputString);
        int counter = matches.Count();
        if (counter != 0)
        {
            parsedPhone = matches[0].Value;
            return true;
        }
        
        Console.WriteLine("Данный телефонный номер невалиден или написан в неподдерживаемом формате ");
        return false;
    }

    public bool TryUpdatePhone(string NewPhone)
    {
        string tmpPhone;
        if (isPhoneValid(NewPhone, out tmpPhone)) 
        {
            phone = tmpPhone;
            Console.WriteLine($"Телефон успешно изменен. Новый номер: {phone}");
            return true;
        }
        else
        {
            Console.WriteLine("Телефонный номер не валиден");
            return false;
        }
    }
}