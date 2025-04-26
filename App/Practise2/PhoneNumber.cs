using System.Text.RegularExpressions;

namespace App.Practise2;

public class PhoneNumber
{
    public static bool TryParsePhone(string inputString, out string parsedPhone) 
    {
        parsedPhone = "";
        string pattern = @"\b(\+?[87])([-\s]*\(?\d{3}\)?[-\s]*\d{3}[-\s]*\d{2}[-\s]*\d{2})\b";
        Regex regex = new Regex(pattern);
        MatchCollection matches  = regex.Matches(inputString);
        int counter = matches.Count();
        if (counter != 0)
        {
            parsedPhone = matches[0].Value;
            Console.WriteLine("Телефонный номер успешно сохранен.");
            return true;
        }
        
        Console.WriteLine("Не удалось найти телефонный номер.");
        return false;
    }
    
    
}
