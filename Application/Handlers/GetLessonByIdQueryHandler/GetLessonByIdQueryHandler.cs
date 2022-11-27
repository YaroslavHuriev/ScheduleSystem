using ScheduleSystem.Application.DTOs;

public class GetLessonByIdQueryHandler : IGetLessonByIdQueryHandler
{
    private readonly ILessonRepository _lessonRepository;

    public GetLessonByIdQueryHandler(ILessonRepository lessonRepository)
    {
        _lessonRepository = lessonRepository;
    }

    public async Task<LessonDto> Handle(string id)
    {
        return await _lessonRepository.GetLessonById(id);
    }
}