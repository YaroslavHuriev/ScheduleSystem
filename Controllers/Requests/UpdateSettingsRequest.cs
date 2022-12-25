using System.ComponentModel.DataAnnotations;

public class UpdateSettingsRequest
{
    [Required]
    public int MaxIterations { get; set; }
    [Required]
    public int PopulationCount { get; set; }
    [Required]
    public int LatestHour { get; set; }
    [Required]
    public int MaxOccurrencesOfOneDisciplinePerDayForGroup { get; set; }
    [Required]
    public int GroupWindowPenalty { get; set; }
    [Required]
    public int TeacherWindowPenalty { get; set; }
    [Required]
    public int LateLessonPenalty { get; set; }
    [Required]
    public int TooMuchOccurrencesOfOneDisciplinePerDayPenalty { get; set; }
}