public interface ITeachersRepository
{
    Task<IEnumerable<TeacherDto>> GetListOfTeachers(string? searchString = null);
    Task CreateTeacher(string id, string firstName, string surname);
    Task DeleteTeacherById(string id);
}
