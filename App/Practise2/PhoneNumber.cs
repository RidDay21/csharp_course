using System.Text.RegularExpressions;

namespace App.Practise2;

public class PhoneNumber
{
    private static readonly string Pattern = @"\b(\+?[87])([-\s]*\(?\d{3}\)?[-\s]*\d{3}[-\s]*\d{2}[-\s]*\d{2})\b";
    private static readonly Regex DigitRegex = new Regex(Pattern, RegexOptions.Compiled);
    public static bool TryParsePhone(string inputString, out string parsedPhone) 
    {
        parsedPhone = "";
        var matches  = DigitRegex.Matches(inputString);
        var counter = matches.Count();
        if (counter != 0)
        {
            parsedPhone = NormalizePhoneNumber(matches[0].Value);
            Console.WriteLine("Телефонный номер успешно сохранен.");
            return true;
        }
        Console.WriteLine("Не удалось найти телефонный номер.");
        return false;
    }

    private static string NormalizePhoneNumber(string inputString)
    {
        var phone = "+7";
        for (var i = 1; i < inputString.Length; i++)
        {
            if (Char.IsDigit(inputString[i])) 
            {
                phone+= inputString[i];
            }    
        }

        return phone;
    }
}
