using System.Xml;

namespace App.Practice3;

/**
 * Что надо сделать?
 *  Мы должны все эти записи в логах агрегировать по месяцам
 * (или по дням, в зависимости от запроса) и положить в один словарик в классе `UserActionStatItem`.
 */
public class UserStatProvider
{
    private UserActionStatResponse _suitableActionsPerDay;
    private UserActionStatResponse _suitableActionsPerMonth;

    public UserStatProvider()
    {
        _suitableActionsPerDay = new UserActionStatResponse()
        {
            UserActionStat = new List<UserActionStatItem>()
        };
        _suitableActionsPerMonth = new UserActionStatResponse()
        {
            UserActionStat = new List<UserActionStatItem>()
        };
        Console.WriteLine("[UserStatProvider] Constructor was called.");
        
    }

    public UserActionStatResponse GetUserActionStat(UserActionStatRequest request,
        List<UserActionItem> userActionItems)
    {
        if (userActionItems == null || userActionItems.Count == 0)
        {
            Console.WriteLine("Исключения");
            return null;
        }
        switch (request.DateGroupType)
        {
            case DateGroupTypes.Daily:
                Console.WriteLine("[GetUserActionStat] Daily Ahalitics were called. Cur terms are:\n" +
                                  $"\tStart: {request.StartDate:dd/MM/yyyy}\n" +
                                  $"\tEnd: {request.EndDate:dd/MM/yyyy}\n" +
                                  $"\tMetrics: {request.DateGroupType}");
                GroupByDays(request, userActionItems);
                return _suitableActionsPerDay;
            case DateGroupTypes.Monthly:
                Console.WriteLine("[GetUserActionStat] Monthly Analitics were called. Cur terms are:\n" +
                                  $"\tStart: {request.StartDate:dd/MM/yyyy}\n" +
                                  $"\tEnd: {request.EndDate:dd/MM/yyyy}\n" +
                                  $"\tMetrics: {request.DateGroupType}");
                GroupByMonths(request, userActionItems);
                return _suitableActionsPerMonth;
            default:
                throw new AbandonedMutexException();
        }
    }

    void GroupByDays(UserActionStatRequest request, List<UserActionItem> userActionItems)
    {
        var strDate = request.StartDate;
        var enDate = request.EndDate;
        foreach (var userActionItem in userActionItems)
        {
            Console.WriteLine("[GroupByDays] Start passing throw userActionItems\n" +
                              $"Cur Element:\n\tName: \"{userActionItem.Action}\"\n\tDate: {userActionItem.Date}\n");
            if (userActionItem.Date >= strDate)
            {
                Console.WriteLine($"[GroupByDays] Daily Analitics were called, because {userActionItem.Date} is after or equal to {strDate.Date}.");
                AddItemToSuitActionsPerDay(userActionItem);
            }
            else if (userActionItem.Date >= enDate)
            {
                break;
            }
        }
    }

    void AddItemToSuitActionsPerDay(UserActionItem item)
    {
        Console.WriteLine($"[AddItemToSuitActionsPerDay] Cur item to add: {item.Action}");
        bool isEmpty = _suitableActionsPerDay.UserActionStat.Count == 0;
        if (isEmpty)
        {
            var dict = new  Dictionary<ActionTypes, int>();
            dict.Add(item.Action, item.Count);
            Console.WriteLine($"[AddItemToSuitActionsPerDay]Create new Dictionary for " +
                              $"ActionMetrics: {dict[item.Action]}.");
            
            var userActionStatItem = new UserActionStatItem();
            userActionStatItem.StartDate = item.Date;
            userActionStatItem.EndDate = item.Date;
            userActionStatItem.ActionMetrics = dict;
            _suitableActionsPerDay.UserActionStat.Add(userActionStatItem);
            Console.WriteLine($"[AddItemToSuitActionsPerDay]Add new pair to " +
                              $"ActionMetrics: {dict[item.Action]}.");
        }
        else if (_suitableActionsPerDay.UserActionStat.Last().StartDate == item.Date)
        {
            Console.WriteLine("[AddItemToSuitActionsPerDay] Item's day has already " +
                              $"exist in _suitableActionPerDay.{item.Action}");
            _suitableActionsPerDay.UserActionStat.Last().ActionMetrics.Add(item.Action, item.Count);
            Console.WriteLine($"[AddItemToSuitActionsPerDay] Item{item.Action} has been succesfully" +
                              $"added to dictionary.");
        } else
        {
            Console.WriteLine("[AddItemToSuitActionsPerDay] Item's day absents" +
                              $"in _suitableActionPerDay.(Item: {item.Action})");
            var dict = new  Dictionary<ActionTypes, int>();
            dict.Add(item.Action, item.Count);
            Console.WriteLine($"[AddItemToSuitActionsPerDay]Create new Dictionary for " +
                              $"ActionMetrics: {dict[item.Action]}.");
            
            var userActionStatItem = new UserActionStatItem();
            userActionStatItem.StartDate = item.Date;
            userActionStatItem.EndDate = item.Date;
            userActionStatItem.ActionMetrics = dict;
            _suitableActionsPerDay.UserActionStat.Add(userActionStatItem);
            Console.WriteLine($"[AddItemToSuitActionsPerDay]Add new pair to " +
                              $"ActionMetrics: {dict[item.Action]}.");
        }
    }
    
    

    void GroupByMonths(UserActionStatRequest request, List<UserActionItem> userActionItems)
    {
        var strDate = request.StartDate;
        var enDate = request.EndDate;
        Console.WriteLine("[GroupByMonths] Start passing throw userActionItems\n");
        foreach (var userActionItem in userActionItems)
        {
            if (userActionItem.Date >= strDate && userActionItem.Date <= enDate)
            {
                Console.WriteLine($"[GroupByMonths] Start to adding Item:\n\t{userActionItem.Action}");
                AddItemToSuitActionsPerMonth(userActionItem, strDate, enDate);
            }
            else if (userActionItem.Date >= enDate)
            {
                break;
            }
        }
    }

    void AddItemToSuitActionsPerMonth(UserActionItem item, DateTime startDate, DateTime endDate)
    {
        Console.WriteLine($"[AddItemToSuitActionsPerMonth] Cur item to add: {item.Action}");
        var date = item.Date;
        if (_suitableActionsPerMonth.UserActionStat.Count == 0)
        {
            Console.WriteLine($"[AddItemToSuitActionsPerMonth] Cur array is empty.Item: {item.Action}");
            var dict = new Dictionary<ActionTypes, int>();
            dict.Add(item.Action, item.Count);
            var end = new DateTime(
                date.Year,
                date.Month,
                DateTime.DaysInMonth(date.Year, date.Month));
            end = end < endDate ? end : endDate;

            int day;
            if (startDate.Month == date.Month)
                day = startDate.Day;
            else
                day = 1;
            var start = new DateTime(
                date.Year,
                date.Month,
                day);
            var userActionStatItem = new UserActionStatItem();
            userActionStatItem.StartDate = start;
            userActionStatItem.EndDate = end;
            userActionStatItem.ActionMetrics = dict;
            _suitableActionsPerMonth.UserActionStat.Add(userActionStatItem);
            Console.WriteLine($"[AddItemToSuitActionsPerMonth] New Montly Array created.Item: {item.Action} has been added to dictionary.");
        } 
        else if (_suitableActionsPerMonth.UserActionStat.Last().EndDate >= item.Date)
        {
            Console.WriteLine($"[AddItemToSuitActionsPerMonth]\n[EndDate >= item.Date] Item: {item.Action} has been added to dictionary.");
            _suitableActionsPerMonth.UserActionStat.Last().ActionMetrics.Add(
                item.Action,
                item.Count);
        }
        else 
        {
            var dict = new Dictionary<ActionTypes, int>();
            dict.Add(item.Action, item.Count);
            var end = new DateTime(
                date.Year,
                date.Month,
                DateTime.DaysInMonth(date.Year, date.Month));
            end = end < endDate ? end : endDate;

            int day;
            if (startDate.Month == date.Month)
                day = startDate.Day;
            else
                day = 1;
            var start = new DateTime(
                date.Year,
                date.Month,
                day);
            var userActionStatItem = new UserActionStatItem();
            userActionStatItem.StartDate = start;
            userActionStatItem.EndDate = end;
            userActionStatItem.ActionMetrics = dict;
            _suitableActionsPerMonth.UserActionStat.Add(userActionStatItem);
            Console.WriteLine($"[AddItemToSuitActionsPerMonth] Item: {item.Action} has been added to dictionary.");
        }
        
    }
}