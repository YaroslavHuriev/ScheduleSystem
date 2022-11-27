using ScheduleSystem.Application.DTOs;

public interface IGetLessonByIdQueryHandler
{
    Task<LessonDto> Handle(string id);
}
