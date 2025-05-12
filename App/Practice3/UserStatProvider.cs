using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace App.Practice3
{
    public class UserStatProvider
    {
        public UserActionStatResponse GetUserActionStat(UserActionStatRequest request,
            List<UserActionItem> userActionItems)
        {
            Logging("Requested user action statistics.");
            Logging($"Grouping type: {request.DateGroupType}, Start: {request.StartDate:dd/MM/yyyy}, End: {request.EndDate:dd/MM/yyyy}");

            UserActionStatResponse suitableActions = new UserActionStatResponse
            {
                UserActionStat = new List<UserActionStatItem>()
            };

            if (userActionItems == null || userActionItems.Count == 0)
            {
                Logging("UserActionItems is null or empty. No data to process.");
                return null;
            }

            switch (request.DateGroupType)
            {
                case DateGroupTypes.Daily:
                    Logging("Processing daily statistics.");
                    GroupItemsByDays(request, userActionItems, ref suitableActions);
                    break;
                case DateGroupTypes.Monthly:
                    Logging("Processing monthly statistics.");
                    GroupItemsByMonths(request, userActionItems, ref suitableActions);
                    break;
                default:
                    Logging($"Unknown DateGroupType: {request.DateGroupType}. Aborting.");
                    throw new AbandonedMutexException("Unknown date group type");
            }

            return suitableActions;
        }

        void GroupItemsByMonths(UserActionStatRequest request, List<UserActionItem> userActionItems,
            ref UserActionStatResponse suitableActions)
        {
            Logging("Grouping items by months.");
            GroupItemsByPeriod(request, userActionItems, ref suitableActions, ShouldCreateNewMontlyItem);
        }

        void GroupItemsByDays(UserActionStatRequest request, List<UserActionItem> userActionItems,
            ref UserActionStatResponse suitableActions)
        {
            Logging("Grouping items by days.");
            GroupItemsByPeriod(request, userActionItems, ref suitableActions, ShouldCreateNewDailyItem);
        }

        bool IsDateInRange(UserActionStatRequest request, DateTime date) =>
            (date.Date >= request.StartDate && date.Date <= request.EndDate);

        void GroupItemsByPeriod(UserActionStatRequest request, List<UserActionItem> userActionItems,
            ref UserActionStatResponse suitableActions,
            Func<UserActionStatResponse, DateTime, bool> termForCreateNewItemInResponse)
        {
            Console.WriteLine("[GroupItemsByPeriod] Processing user actions within date range...");

            foreach (var userActionItem in userActionItems)
            {
                if (!IsDateInRange(request, userActionItem.Date))
                {
                    Console.WriteLine($"[GroupItemsByPeriod] Skipping action outside date range: {userActionItem.Date:dd/MM/yyyy}");
                    continue;
                }

                Console.WriteLine($"[GroupItemsByPeriod] Handling action: {userActionItem.Action} on {userActionItem.Date:dd/MM/yyyy}");
                AddItemToSuitActions(userActionItem, request, suitableActions, termForCreateNewItemInResponse);
            }
        }

        bool TermForMontlyRequest(UserActionStatResponse response, DateTime date) =>
            (response.UserActionStat.Last().StartDate.Month != date.Month);

        bool TermForDailyRequest(UserActionStatResponse response, DateTime date) =>
            (response.UserActionStat.Last().StartDate.Day != date.Day);

        bool ShouldCreateNewDailyItem(UserActionStatResponse suitableActions, DateTime date)
        {
            Console.WriteLine($"[ShouldCreateNewDailyItem] Checking for new daily group on {date:dd/MM/yyyy}");
            return suitableActions.UserActionStat.Count == 0 || TermForDailyRequest(suitableActions, date);
        }

        bool ShouldCreateNewMontlyItem(UserActionStatResponse suitableActions, DateTime date)
        {
            Console.WriteLine($"[ShouldCreateNewMontlyItem] Checking for new monthly group on {date:MM/yyyy}");
            return suitableActions.UserActionStat.Count == 0 || TermForMontlyRequest(suitableActions, date);
        }

        void AddItemToSuitActions(UserActionItem item, UserActionStatRequest request, UserActionStatResponse suitableActions,
            Func<UserActionStatResponse, DateTime, bool> termForCreateNewItemInResponse)
        {
            Console.WriteLine($"[AddItemToSuitActions] Preparing to add action: {item.Action}, Count: {item.Count}, Date: {item.Date:dd/MM/yyyy}");

            if (termForCreateNewItemInResponse(suitableActions, item.Date))
            {
                var dict = new Dictionary<ActionTypes, int> { { item.Action, item.Count } };
                Console.WriteLine($"[AddItemToSuitActions] New date range detected. Creating new action metrics entry for {item.Date:dd/MM/yyyy}.");

                DateTime start, end;
                if (request.DateGroupType == DateGroupTypes.Monthly)
                {
                    start = DateCalculator.GetStartDay(item.Date, request.StartDate);
                    end = DateCalculator.GetEndDay(item.Date, request.EndDate);
                }
                else
                {
                    start = end = item.Date;
                }

                var userActionStatItem = new UserActionStatItem
                {
                    StartDate = start,
                    EndDate = end,
                    ActionMetrics = dict
                };

                suitableActions.UserActionStat.Add(userActionStatItem);
                Console.WriteLine($"[AddItemToSuitActions] Action {item.Action} with count {item.Count} added to new date group.");
            }
            else
            {
                Console.WriteLine($"[AddItemToSuitActions] Existing date group found. Appending action {item.Action} with count {item.Count}.");
                if (!suitableActions.UserActionStat.Last().ActionMetrics.ContainsKey(item.Action))
                    suitableActions.UserActionStat.Last().ActionMetrics[item.Action] = item.Count;
                else
                    suitableActions.UserActionStat.Last().ActionMetrics[item.Action] += item.Count;

                Console.WriteLine($"[AddItemToSuitActions] Action {item.Action} successfully added to existing group.");
            }
        }

        static class DateCalculator
        {
            public static DateTime GetStartDay(DateTime date, DateTime startDate)
            {
                int day = (startDate.Month == date.Month) ? startDate.Day : 1;
                return new DateTime(date.Year, date.Month, day);
            }

            public static DateTime GetEndDay(DateTime date, DateTime endDate)
            {
                var end = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
                return end < endDate ? end : endDate;
            }
        }

        static void Logging(string message)
        {
            string methodName = "[" + new StackTrace().GetFrame(1).GetMethod().Name + "]\t";
            Console.WriteLine(methodName + message);
        }
    }
}
