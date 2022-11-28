
public class DeleteGroupUseCase : IDeleteGroupUseCase
{
    private readonly IGroupRepository _groupRepository;

    public DeleteGroupUseCase(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task Execute(string id)
    {
        await _groupRepository.DeleteGroupById(id);
    }
}