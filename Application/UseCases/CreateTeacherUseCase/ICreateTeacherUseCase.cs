public interface ICreateTeacherUseCase
{
    Task<string> Execute(CreateTeacherUseCaseInput input);
}
