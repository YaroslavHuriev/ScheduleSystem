public class TeacherDbo
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }

    public TeacherDto ToDto()
    {
        return new TeacherDto
        {
            Id = Id.ToString(),
            FirstName = FirstName,
            Surname = Surname
        };
    }
}