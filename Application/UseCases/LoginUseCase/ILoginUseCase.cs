public interface ILoginUseCase
{
    Task<string?> Execute(LoginUseCaseInput input);
}
