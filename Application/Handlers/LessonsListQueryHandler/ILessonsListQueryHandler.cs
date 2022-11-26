using ScheduleSystem.Application.DTOs;

interface ILessonsListQueryHandler
{
    Task<IEnumerable<LessonDto>> Handle(string inputDataId);
}
