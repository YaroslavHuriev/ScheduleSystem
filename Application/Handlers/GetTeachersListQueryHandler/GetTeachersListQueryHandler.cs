
public class GetTeachersListQueryHandler : IGetTeachersListQueryHandler
{
    private readonly ITeachersRepository _teachersRepository;

    public GetTeachersListQueryHandler(ITeachersRepository teachersRepository)
    {
        _teachersRepository = teachersRepository;
    }

    public async Task<IEnumerable<TeacherDto>> Handle(string? searchString = null)
    {
        return await _teachersRepository.GetListOfTeachers(searchString);
    }
}