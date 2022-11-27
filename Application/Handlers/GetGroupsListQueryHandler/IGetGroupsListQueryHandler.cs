public interface IGetGroupsListQueryHandler
{
    Task<IEnumerable<GroupDto>> Handle(string? searchString = null);
}
