public class TeacherDbo
{
    public Guid Id { get; set; }
    public string FullName { get; set; }

    public TeacherDto ToDto()
    {
        return new TeacherDto
        {
            Id = Id.ToString(),
            FullName = FullName
        };
    }
}