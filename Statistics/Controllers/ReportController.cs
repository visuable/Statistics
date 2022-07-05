using Microsoft.AspNetCore.Mvc;

using Statistics.Units.ReportRequestUnit;

using static Statistics.Units.ReportRequestUnit.IReportRequestUnit;

namespace Statistics.Controllers
{
    [ApiController]
    [Route("report")]
    public class ReportController : ControllerBase
    {
        private readonly IReportRequestUnit _reportUnit;

        public ReportController(IReportRequestUnit reportUnit)
        {
            _reportUnit = reportUnit;
        }

        [HttpPost("user_statistics")]
        public async Task<IActionResult> CreateReport([FromBody] CreateReportRequestModel model)
        {
            return Ok(await _reportUnit.CreateReportRequest(model));
        }

        [HttpGet("info")]
        public async Task<IActionResult> GetReportInfo([FromQuery] GetReportInfoModel model)
        {
            return Ok(await _reportUnit.GetReportInfo(model));
        }
    }
}
