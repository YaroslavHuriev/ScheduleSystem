
public class CreateGroupUseCase : ICreateGroupUseCase
{
    private readonly IGroupRepository _groupRepository;

    public CreateGroupUseCase(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<string> Execute(string name)
    {
        var id = Guid.NewGuid().ToString();
        await _groupRepository.CreateGroup(id, name);
        return id;
    }
}