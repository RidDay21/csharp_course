using System.Text;
using System.Text.RegularExpressions;

namespace App.Practise2;

public class StackMachine
{
    // file: StackMachine.cs
    public static string CalculateString(string[] codeLines) 
    {
        var sb = new StringBuilder("");
        foreach (string line in codeLines)
        {
            if (line.StartsWith("pop"))
            {
                var charsToRemove = int.Parse(line.Substring(4));
                sb.Remove(sb.Length - charsToRemove, charsToRemove);
            }

            else
            {
                sb.Append(line.Substring(5));
            }
        }
        var result = sb.ToString();
        return result;
    }
}

