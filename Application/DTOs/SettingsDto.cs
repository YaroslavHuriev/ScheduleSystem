public class SettingsDto
{
    public int MaxIterations { get; set; }
    public int PopulationCount { get; set; }
    public int LatestHour { get; set; }
    public int MaxOccurrencesOfOneDisciplinePerDayForGroup { get; set; }
    public int GroupWindowPenalty { get; set; }
    public int TeacherWindowPenalty { get; set; }
    public int LateLessonPenalty { get; set; }
    public int TooMuchOccurrencesOfOneDisciplinePerDayPenalty { get; set; }
}