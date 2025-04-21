using System.Text.RegularExpressions;

namespace App.Practise2;

public class Benford
{
    public static int[] GetBenfordStatistics(string text)
    {
        var statistics = new int[10];
        string pattern = @"\b\d+\b";
        Regex regex = new Regex(pattern);
        MatchCollection matches = regex.Matches(text);
        foreach (Match match in matches)
        {
            string number = match.Value;
            if (!string.IsNullOrEmpty(number))
            {
                if (number[0] != '0')
                {
                    int index = number[0] - '0';
                    statistics[index]++;
                }
            }
        }
        
        return statistics;
    }
}