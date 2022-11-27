using ScheduleSystem.Application.DTOs;

public interface ILessonRepository
{
    Task<IEnumerable<LessonDto>> GetLessonsByInputDataId(string inputDataId);
    Task<string> CreateLesson(Guid id, string groupId, string teacherId, string discipline, string inputDataId, int room);
    Task<LessonDto> GetLessonById(string id);
}
