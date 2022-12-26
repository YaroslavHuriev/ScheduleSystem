using System.Data;
using Dapper;

public class UserRepository : IUserRepository
{
    private readonly IDbConnection _connection;
    public UserRepository(IDbConnection connection)
    {
        _connection = connection;
    }
    public async Task<UserDTO> GetUser(string email)
    {
        if (_connection.State != ConnectionState.Open)
        {
            _connection.Open();
        }
        var query = @$"SELECT ""UserName"", ""Password"", schedule.""Role"".""Name"" as ""Role""
	        FROM schedule.""User"" INNER JOIN schedule.""Role"" ON schedule.""User"".""RoleId""=schedule.""Role"".""Id""
            WHERE schedule.""User"".""UserName""=@Email;";
        var command = new CommandDefinition(query, new {Email = email});
        var dto = await _connection.QueryFirstOrDefaultAsync<UserDTO>(command);
        _connection.Close();
        return dto;
    }

    public async Task CreateUser(string username, string password)
    {
        if (_connection.State != ConnectionState.Open)
        {
            _connection.Open();
        }
        var query = @"INSERT INTO schedule.""User""(
	        ""Id"", ""UserName"", ""Password"", ""RoleId"")
	        VALUES (@Id, @Username, @Password, '2be75535-d6b7-469e-aaa0-ec7efe06775f');";
        var command = new CommandDefinition(query, new
        {
            Id = Guid.NewGuid(),
            Username = username,
            Password = password
        });
        await _connection.ExecuteAsync(command);
        _connection.Close();
    }
}
