public interface IGetTeachersListQueryHandler
{
    Task<IEnumerable<TeacherDto>> Handle(string? searchString = null);
}
