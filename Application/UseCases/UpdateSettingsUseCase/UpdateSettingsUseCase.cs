
public class UpdateSettingsUseCase : IUpdateSettingsUseCase
{
    private readonly ISettingsRepository _settingsRepository;

    public UpdateSettingsUseCase(ISettingsRepository settingsRepository)
    {
        _settingsRepository = settingsRepository;
    }

    public async Task Execute(UpdateSettingsInput input)
    {
        await _settingsRepository.UpdateSettings(input.MaxIterations, input.PopulationCount, input.LatestHour, input.MaxOccurrencesOfOneDisciplinePerDayForGroup, input.GroupWindowPenalty, input.TeacherWindowPenalty, input.LateLessonPenalty, input.TooMuchOccurrencesOfOneDisciplinePerDayPenalty);
    }
}