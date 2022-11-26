using ScheduleSystem.Application.DTOs;

public interface ILessonRepository
{
    Task<IEnumerable<LessonDto>> GetLessonsByInputDataId(string inputDataId);
}
