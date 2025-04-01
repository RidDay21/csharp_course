using System.Text.RegularExpressions;

namespace App.Practise2;

public class Benford
{
    //[TestCase("123_456_789", new int[] {1,0,0,1,0,0,1,0,0})]
    private static readonly string Pattern = @"\b\d+\b";
    private static readonly Regex DigitRegex = new Regex(Pattern, RegexOptions.Compiled); 
    public static int[] GetBenfordStatistics(string text)
    {
        var statistics = new int[9];
        var textWithoutUnderlining = text.Split("_");
        foreach (var line in textWithoutUnderlining)
        {
            var matches = DigitRegex.Matches(line);
            foreach (var match in matches)
            {
                var number = match.ToString();
                if (!string.IsNullOrEmpty(number))
                {
                    if (number[0] != '0')
                    {
                        var index = number[0] - '0' - 1;
                        statistics[index]++;
                    }
                }
            }
        }
        return statistics;
    }
}