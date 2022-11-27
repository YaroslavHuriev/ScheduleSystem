using ScheduleSystem.Application.DTOs;

public interface ILessonsListQueryHandler
{
    Task<IEnumerable<LessonDto>> Handle(string inputDataId);
}
