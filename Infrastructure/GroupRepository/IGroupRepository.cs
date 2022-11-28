public interface IGroupRepository
{
    Task<IEnumerable<GroupDto>> GetGroupsList(string? searchString = null);
    Task CreateGroup(string id, string name);
    Task DeleteGroupById(string id);
}
