public class GroupDbo
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public GroupDto ToDto()
    {
        return new GroupDto
        {
            Id = Id.ToString(),
            Name = Name
        };
    }
}