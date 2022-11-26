using ScheduleSystem.Application.DTOs;

class LessonsListQueryHandler : ILessonsListQueryHandler
{
    private readonly ILessonRepository _lessonRepository;

    public LessonsListQueryHandler(ILessonRepository lessonRepository)
    {
        _lessonRepository = lessonRepository;
    }

    public async Task<IEnumerable<LessonDto>> Handle(string inputDataId)
    {
        return await _lessonRepository.GetLessonsByInputDataId(inputDataId);
    }
}