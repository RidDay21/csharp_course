using System.Globalization;
using System.Text.RegularExpressions;

namespace App.Practice3;

public class User
{
    private readonly Guid id;
    private readonly string login;
    private readonly string password;
    private readonly string name;
    private readonly string surname;
    private readonly string inn;
    private string phone;
    private readonly DateTime registerDate;

    public string Phone
    {
        get
        {
            return  phone;
        }
        set
        {
            TryUpdatePhone(value);
        }
    }
    
    public string Name { get; init; }
    public string Surname { get; init; }
    public string INN { get; init; }
    public string Login { get; init; }
    public string Password { get; init; }
    public Guid ID { get; init; }
    public DateTime RegisterTime { get; init; }
    

    public User(string login, string password, string name, string surname, string inn, string phone)
    {
        this.login = login;
        this.password = password;
        this.name = name;
        this.surname = surname;
        this.inn = inn;
        this.phone = phone;
    }

    public User()
    {
        this.id = Guid.NewGuid();
        this.registerDate =  DateTime.Now;
    }

    public string GetUserFullName()
    {
        return $"User: {name} {surname}";
    }
    static bool isPhoneValid(string inputString, out string parsedPhone) 
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

    private bool TryUpdatePhone(string NewPhone)
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