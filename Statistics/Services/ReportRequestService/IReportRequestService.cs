using Statistics.Entities;

namespace Statistics.Services.ReportRequestService
{
    public interface IReportRequestService
    {
        Task UpdateStatus(Guid reportRequestId, RequestStatus status);
        Task SetReport(Guid reportRequestId, Guid reportId);
        Task<ICollection<UnprocessedReportRequestModel>> GetUnprocessed();

        public class UnprocessedReportRequestModel
        {
            public Guid Id { get; set; }
            public Guid UserId { get; set; }
            public DateTime From { get; set; }
            public DateTime To { get; set; }
        }
    }
}
