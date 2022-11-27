using System.Data;
using Dapper;

public class GroupRepository : IGroupRepository
{
    private readonly IDbConnection _connection;

    public GroupRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<IEnumerable<GroupDto>> GetGroupsList(string? searchString = null)
    {
        if (_connection.State != ConnectionState.Open)
        {
            _connection.Open();
        }
        var query = @"SELECT ""Id"", ""Name""
	        FROM schedule.""Group""";
        if (!string.IsNullOrEmpty(searchString))
        {
            query += @$"WHERE schedule.""Group"".""Name"" LIKE '%{searchString}%'";
        }
        query += ";";
        var command = new CommandDefinition(query);
        var dbos = await _connection.QueryAsync<GroupDbo>(command);
        _connection.Close();
        return dbos.Select(dbo => dbo.ToDto());
    }
}