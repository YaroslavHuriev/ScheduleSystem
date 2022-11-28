public interface ICreateGroupUseCase
{
    Task<string> Execute(string name);
}
