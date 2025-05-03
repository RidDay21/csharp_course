namespace App.Practice3;

/**
 * Что надо сделать?
 *  Мы должны все эти записи в логах агрегировать по месяцам
 * (или по дням, в зависимости от запроса) и положить в один словарик в классе `UserActionStatItem`.
 */
public class UserStatProvider
{
    public UserActionStatResponse GetUserActionStat(UserActionStatRequest request, List<UserActionItem> userActionItems)
    {
        
        return null;
    }

    
    
    /**
     * Method for checking is our date in TimeSpan or Not
     */
    bool IsInSpan(DateTime date, DateTime begin, DateTime end)
    {
        return date > begin && date < end  ? true : false;
    }
    
}

  