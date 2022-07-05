using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

using Statistics.Units.ReportRequestUnit;

using static Statistics.Units.ReportRequestUnit.IReportRequestUnit;

namespace Statistics.Controllers;

[ApiController]
[Route("report")]
[Produces(MediaTypeNames.Application.Json)]
public class ReportController : ControllerBase
{
    private readonly IReportRequestUnit _reportUnit;

    public ReportController(IReportRequestUnit reportUnit)
    {
        _reportUnit = reportUnit;
    }

    /// <summary>
    /// Создает заявку на получение статистики пользователя
    /// </summary>
    /// <remarks>Время указывается везде по UTC</remarks>
    /// <param name="model">Модель создания заявки</param>
    /// <returns>Модель идентификатора заявки</returns>
    [HttpPost("user_statistics")]
    public async Task<IActionResult> CreateReport([FromBody] CreateReportRequestModel model)
    {
        return Ok(await _reportUnit.CreateReportRequest(model));
    }

    /// <summary>
    /// Получает информацию о заявке 
    /// </summary>
    /// <param name="model">Модель отправки запроса</param>
    /// <returns>Модель состояния заявки</returns>
    [HttpGet("info")]
    public async Task<IActionResult> GetReportInfo([FromQuery] GetReportInfoModel model)
    {
        return Ok(await _reportUnit.GetReportInfo(model));
    }
}