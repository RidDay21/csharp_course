using System.Globalization;
using App.Practice3;

namespace App;

public static class Program
{
    public static void Main()
    {
        var user1 = new User(
            "Rid21",
            "0487",
            "Nikolay",
            "Laptev",
            "7830002293",
            "8-913-777-77-77");
        var user2 = new User(
            "GRUSHA",
            "mcl4r3n",
            "Grigoriy",
            "Kozlov",
            "7703399790",
            "+7(934) 785-23-12");
        Console.WriteLine(user1.GetUserFullName());
        Console.WriteLine(user2.GetUserFullName());

        //string invalidPhoneNumber = "7913 777 77 777";
        var validPhoneNumber = "+7 (913) 876 54 32";

        User user3 = UserCreator.CreateUser(
            "boris",
            "12d33f",
            "kirill",
            "kochanov",
            "7703399790",
            validPhoneNumber);
        Console.WriteLine(user3.PasswordHash);

        user3.Phone = "+8-913-780-04-32";
        Console.WriteLine(user3.GetUserFullName());

        // Подготовка тестовых данных
        var testData = new List<UserActionItem>
        {
            new UserActionItem { Date = new DateTime(2024, 12, 9), Action = ActionTypes.Login, Count = 1 },
            new UserActionItem { Date = new DateTime(2024, 12, 10), Action = ActionTypes.SearchProducts, Count = 3 },
            new UserActionItem
                { Date = new DateTime(2024, 12, 10), Action = ActionTypes.GetProductDetails, Count = 12 },
            new UserActionItem { Date = new DateTime(2024, 12, 10), Action = ActionTypes.AddProductToCart, Count = 2 },
            new UserActionItem { Date = new DateTime(2025, 1, 1), Action = ActionTypes.PayOrder, Count = 1 },
            new UserActionItem { Date = new DateTime(2025, 1, 15), Action = ActionTypes.RecieveOrder, Count = 1 }
        };

        var provider = new UserStatProvider();

        // Тест 1: Daily группировка
        Console.WriteLine("Тест 1: Daily группировка");
        var dailyRequest = new UserActionStatRequest
        {
            StartDate = new DateTime(2024, 12, 10),
            EndDate = new DateTime(2025, 1, 30),
            DateGroupType = DateGroupTypes.Daily
        };
        Console.WriteLine($"[dailyRequest] \n\t{dailyRequest.StartDate}\n\t{dailyRequest.EndDate}" +
                          $"\n\t{dailyRequest.DateGroupType}");

        var dailyResult = provider.GetUserActionStat(dailyRequest, testData);
        PrintResult(dailyResult);
        
        Console.WriteLine("\nТест 2: Monthly группировка");
        var monthlyRequest = new UserActionStatRequest
        {
            StartDate = new DateTime(2024, 12, 9),
            EndDate = new DateTime(2025, 1, 14),
            DateGroupType = DateGroupTypes.Monthly
        };

        var monthlyResult = provider.GetUserActionStat(monthlyRequest, testData);
        PrintResult(monthlyResult);
        
    }

    static void PrintResult(UserActionStatResponse response)
    {
        Console.WriteLine("[PrintResult] Function was called\n" +
                          $"Length of responce:{response.UserActionStat.Count}");
        foreach (var item in response.UserActionStat)
        {
            Console.WriteLine($"Start: {item.StartDate:dd-MM-yy}");
            Console.WriteLine($"End: {item.EndDate:dd-MM-yy}");
            Console.WriteLine("Metrics:");
            foreach (var metric in item.ActionMetrics)
            {
                Console.WriteLine($"  {metric.Key}: {metric.Value}");
            }

            Console.WriteLine();
        }
    }
}