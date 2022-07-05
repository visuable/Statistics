namespace Statistics.Entities
{
    public class ReportRequest : BaseEntity
    {
        public Guid RequestId { get; set; }
        public Guid? ReportId { get; set; }
        public Guid UserId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public Request Request { get; set; }
        public Report Report { get; set; }
        public User User { get; set; }

        public class Builder
        {
            private readonly ReportRequest _reportRequest = new();

            public Builder WithRequest()
            {
                _reportRequest.Request = new Request()
                {
                    CreatedAt = DateTime.UtcNow,
                    Status = RequestStatus.Created,
                    UpdatedAt = DateTime.UtcNow
                };
                return this;
            }

            public Builder WithRange(DateTime from, DateTime to)
            {

                _reportRequest.From = from;
                _reportRequest.To = to;
                return this;
            }

            public Builder ToUser(Guid userId)
            {
                _reportRequest.UserId = userId;
                return this;
            }

            public ReportRequest Build()
            {
                return _reportRequest;
            }
        }
    }
}
