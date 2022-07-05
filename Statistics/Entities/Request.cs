namespace Statistics.Entities;

public class Request : BaseEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public RequestStatus Status { get; set; }
}

public enum RequestStatus
{
    Created,
    Processing,
    Ready
}