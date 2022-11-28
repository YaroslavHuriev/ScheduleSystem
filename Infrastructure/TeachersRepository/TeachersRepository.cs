using System.Data;
using Dapper;

public class TeachersRepository : ITeachersRepository
{
    private readonly IDbConnection _connection;

    public TeachersRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<IEnumerable<TeacherDto>> GetListOfTeachers(string? searchString = null)
    {
        if (_connection.State != ConnectionState.Open)
        {
            _connection.Open();
        }
        var query = @"SELECT ""Id"", CONCAT(""Surname"", ' ', ""FirstName"") as ""FullName""
	        FROM schedule.""Teacher""";
        if (!string.IsNullOrEmpty(searchString))
        {
            query += @$"WHERE CONCAT(""Surname"", ' ', ""FirstName"") LIKE '%{searchString}%'";
        }
        query += ";";
        var command = new CommandDefinition(query);
        var dbos = await _connection.QueryAsync<TeacherDbo>(command);
        _connection.Close();
        return dbos.Select(dbo => dbo.ToDto());
    }

    public async Task CreateTeacher(string id, string firstName, string surname)
    {
        if (_connection.State != ConnectionState.Open)
        {
            _connection.Open();
        }
        var query = @$"INSERT INTO schedule.""Teacher""(
	        ""Id"", ""FirstName"", ""Surname""))
            VALUES ('{id}', '{firstName}, '{surname}');";
        var command = new CommandDefinition(query);
        await _connection.ExecuteAsync(command);
        _connection.Close();
    }

    public async Task DeleteTeacherById(string id)
    {
        if (_connection.State != ConnectionState.Open)
        {
            _connection.Open();
        }
        var query = @$"DELETE FROM schedule.""Teacher""
	        WHERE schedule.""Teacher"".""Id""='{id}';";
        var command = new CommandDefinition(query);
        await _connection.ExecuteAsync(command);
        _connection.Close();
    }
}