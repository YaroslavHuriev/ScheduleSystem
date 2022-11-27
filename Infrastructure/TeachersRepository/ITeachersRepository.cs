public interface ITeachersRepository
{
    Task<IEnumerable<TeacherDto>> GetListOfTeachers(string? searchString = null);
}
