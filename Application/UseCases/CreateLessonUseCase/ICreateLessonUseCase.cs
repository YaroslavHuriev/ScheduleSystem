public interface ICreateLessonUseCase
{
    Task<string> Execute(CreateLessonUseCaseInput input);
}
