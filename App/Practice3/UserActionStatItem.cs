namespace App.Practice3;

public class UserActionStatItem  
{  
    public DateTime StartDate { get; set; }  
    public DateTime EndDate { get; set; }  
    public Dictionary<ActionTypes, int> ActionMetrics { get; set; }  
}  