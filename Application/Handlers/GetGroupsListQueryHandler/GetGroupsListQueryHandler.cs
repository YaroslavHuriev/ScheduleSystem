
public class GetGroupsListQueryHandler : IGetGroupsListQueryHandler
{
    private readonly IGroupRepository _groupRepository;

    public GetGroupsListQueryHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<IEnumerable<GroupDto>> Handle(string? searchString = null)
    {
        return await _groupRepository.GetGroupsList(searchString);
    }
}