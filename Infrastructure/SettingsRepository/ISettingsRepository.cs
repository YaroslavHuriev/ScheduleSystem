public interface ISettingsRepository
{
    Task<SettingsDto> GetSettings();
    Task UpdateSettings(int maxIterations, int populationCount, int latestHour, int maxOccurrencesOfOneDisciplinePerDayForGroup, int groupWindowPenalty, int teacherWindowPenalty, int lateLessonPenalty, int tooMuchOccurrencesOfOneDisciplinePerDayPenalty);
}
