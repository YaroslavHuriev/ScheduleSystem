
public class GetSettingsQueryHandler : IGetSettingsQueryHandler
{
    private readonly ISettingsRepository _settingsRepository;

    public GetSettingsQueryHandler(ISettingsRepository settingsRepository)
    {
        _settingsRepository = settingsRepository;
    }

    public async Task<SettingsDto> Handle()
    {
        return await _settingsRepository.GetSettings();
    }
}