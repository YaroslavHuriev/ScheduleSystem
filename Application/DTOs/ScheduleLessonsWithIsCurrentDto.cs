using ScheduleSystem.Application.DTOs;

public class ScheduleLessonsWithIsCurrentDto
{
    public IEnumerable<LessonWithTimeDto> Lessons { get; set; }
    public bool IsCurrent { get; set; }
}