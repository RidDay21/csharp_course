using App.Practise2;

namespace App;

public static class Program
{
    public static void Main()
    {
        string[] lines = new string[] { "pop 24323"};
        Console.WriteLine(StackMachine.CalculateString(lines));
        Console.WriteLine("Hello, world!");
    }
}