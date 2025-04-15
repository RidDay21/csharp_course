namespace App.Practise2;

public class Benford
{
 
    // file: Benford.cs
    static int[] GetBenfordStatistics(string text)
    {
        var statistics = new int[10];
        Array.Fill(statistics, 0);
        string[] message = text.Split(' ');
        for (int i = 0; i < message.Length; i++)
        {
            if (message[i].All(char.IsDigit) && message[i][0] != '0')
            {
                statistics[message[i][0] - '0']++;
            }
        }
        return statistics;
    }
}