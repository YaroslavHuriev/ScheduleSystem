
public class CreateTeacherUseCase : ICreateTeacherUseCase
{
    private readonly ITeachersRepository _teachersRepository;

    public CreateTeacherUseCase(ITeachersRepository teachersRepository)
    {
        _teachersRepository = teachersRepository;
    }

    public async Task<string> Execute(CreateTeacherUseCaseInput input)
    {
        var id = Guid.NewGuid().ToString();
        await _teachersRepository.CreateTeacher(id, input.FirstName, input.Surname);
        return id;
    }
}