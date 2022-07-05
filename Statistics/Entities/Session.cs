namespace Statistics.Entities;

public class Session : BaseEntity
{
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public User User { get; set; }

    public class Builder
    {
        private readonly Session _session = new() { CreatedAt = DateTime.UtcNow };

        public Builder WithUser(Guid userId)
        {
            _session.UserId = userId;
            return this;
        }

        public Session Build()
        {
            return _session;
        }
    }
}