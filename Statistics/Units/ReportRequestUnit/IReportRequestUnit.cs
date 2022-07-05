namespace Statistics.Units.ReportRequestUnit;

public interface IReportRequestUnit
{
    Task<CreatedReportRequestModel> CreateReportRequest(CreateReportRequestModel model);
    Task<ReportInfoStatusModel> GetReportInfo(GetReportInfoModel model);

    public class CreateReportRequestModel
    {
        public Guid UserId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }

    public class CreatedReportRequestModel
    {
        public Guid RequestId { get; set; }
    }

    public class GetReportInfoModel
    {
        public Guid RequestId { get; set; }
    }

    public class ReportInfoStatusModel
    {
        public Guid RequestId { get; set; }
        public decimal Percent { get; set; }
        public ReportInfoModel? Result { get; set; }

        public class ReportInfoModel
        {
            public int CountSignIn { get; set; }
            public Guid UserId { get; set; }
        }
    }
}