public interface IGroupRepository
{
    Task<IEnumerable<GroupDto>> GetGroupsList(string? searchString = null);
}
