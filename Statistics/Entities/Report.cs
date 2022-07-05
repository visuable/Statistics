namespace Statistics.Entities;

public class Report : BaseEntity
{
    public int CountSignIn { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}