
public class DeleteTeacherUseCase : IDeleteTeacherUseCase
{
    private readonly ITeachersRepository _teachersRepository;

    public DeleteTeacherUseCase(ITeachersRepository teachersRepository)
    {
        _teachersRepository = teachersRepository;
    }

    public async Task Execute(string id)
    {
        await _teachersRepository.DeleteTeacherById(id);
    }
}