using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
[ApiController]
public class SettingsController : ControllerBase
{
    private readonly IGetSettingsQueryHandler _getSettingsQueryHandler;
    private readonly IUpdateSettingsUseCase _updateSettingsUseCase;

    public SettingsController(IGetSettingsQueryHandler getSettingsQueryHandler, IUpdateSettingsUseCase updateSettingsUseCase)
    {
        _getSettingsQueryHandler = getSettingsQueryHandler;
        _updateSettingsUseCase = updateSettingsUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetSettings()
    {
        var result = await _getSettingsQueryHandler.Handle();
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSettings(UpdateSettingsRequest request)
    {
        await _updateSettingsUseCase.Execute(new UpdateSettingsInput
        {
            GroupWindowPenalty = request.GroupWindowPenalty,
            LateLessonPenalty = request.LateLessonPenalty,
            LatestHour = request.LatestHour,
            MaxIterations = request.MaxIterations,
            MaxOccurrencesOfOneDisciplinePerDayForGroup = request.MaxOccurrencesOfOneDisciplinePerDayForGroup,
            PopulationCount = request.PopulationCount,
            TeacherWindowPenalty = request.TeacherWindowPenalty,
            TooMuchOccurrencesOfOneDisciplinePerDayPenalty = request.TooMuchOccurrencesOfOneDisciplinePerDayPenalty
        });
        return Ok();
    }
}