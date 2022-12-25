public class SettingsDbo
{
    public Guid Id { get; set; }
    public int MaxIterations { get; set; }

    public int PopulationCount { get; set; }

    public int LatestHour { get; set; }

    public int MaxOccurrencesOfOneDisciplinePerDayForGroup { get; set; }
    public int GroupWindowPenalty { get; set; }
    public int TeacherWindowPenalty { get; set; }
    public int LateLessonPenalty { get; set; }
    public int TooMuchOccurrencesOfOneDisciplinePerDayPenalty { get; set; }

    public SettingsDto ToDto()
    {
        return new SettingsDto
        {
            GroupWindowPenalty = GroupWindowPenalty,
            LateLessonPenalty = LateLessonPenalty,
            LatestHour = LatestHour,
            MaxIterations = MaxIterations,
            MaxOccurrencesOfOneDisciplinePerDayForGroup = MaxOccurrencesOfOneDisciplinePerDayForGroup,
            PopulationCount = PopulationCount,
            TeacherWindowPenalty = TeacherWindowPenalty,
            TooMuchOccurrencesOfOneDisciplinePerDayPenalty = TooMuchOccurrencesOfOneDisciplinePerDayPenalty
        };
    }
}