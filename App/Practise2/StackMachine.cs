using System.Text;
using System.Text.RegularExpressions;

namespace App.Practise2;

public class StackMachine
{
    // file: StackMachine.cs
    public static string CalculateString(string[] codeLines) 
    {
        StringBuilder sb = new StringBuilder("");
        foreach (string line in codeLines)
        {
            if (line.IndexOf("pop") == 0)
            {
                string pattern = @"\b\d+\b";
                Regex regex = new Regex(pattern);
                MatchCollection matches = regex.Matches(line);
                int charsToRemove = int.Parse(matches[0].Value);
                sb.Remove(sb.Length - charsToRemove, charsToRemove);
            }

            if (line.IndexOf("push") == 0)
            {
                sb.Append(line.Substring(5));
            }
        }
        string result = sb.ToString();
        return result;
    }
}

