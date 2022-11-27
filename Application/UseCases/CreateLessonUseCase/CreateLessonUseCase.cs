
public class CreateLessonUseCase : ICreateLessonUseCase
{
    private readonly ILessonRepository _lessonRepository;

    public CreateLessonUseCase(ILessonRepository lessonRepository)
    {
        _lessonRepository = lessonRepository;
    }

    public async Task<string> Execute(CreateLessonUseCaseInput input)
    {
        return await _lessonRepository.CreateLesson(Guid.NewGuid(), input.GroupId, input.TeacherId, input.Discipline, input.InputDataId, input.Room);
    }
}