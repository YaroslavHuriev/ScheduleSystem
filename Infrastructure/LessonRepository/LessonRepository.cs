using System.Data;
using Dapper;
using ScheduleSystem.Application.DTOs;

class LessonRepository : ILessonRepository
{
    private readonly IDbConnection _connection;
    public LessonRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<IEnumerable<LessonDto>> GetLessonsByInputDataId(string inputDataId)
    {
        if (_connection.State != ConnectionState.Open)
        {
            _connection.Open();
        }
        var query = @$"SELECT ""Group"".""Name"" as ""Group"",""Teacher"".""Surname"" as ""Teacher"", ""Room"" , ""Discipline"", ""Lessons"".""Id"" as ""Id""
				FROM schedule.""Lessons"" as ""Lessons""
				INNER JOIN schedule.""Teacher"" as ""Teacher"" on ""Lessons"".""TeacherId"" = ""Teacher"".""Id""
				INNER JOIN schedule.""Group"" as ""Group"" on ""Lessons"".""GroupId""=""Group"".""Id""
				WHERE ""Lessons"".""inputdataid""='{inputDataId}';";
        var command = new CommandDefinition(query);
        var dbos = await _connection.QueryAsync<LessonDbo>(command);
        _connection.Close();
        return dbos.Select(dbo => dbo.ToDto());
    }

    public async Task<LessonDto> GetLessonById(string id)
    {
        if (_connection.State != ConnectionState.Open)
        {
            _connection.Open();
        }
        var query = @$"SELECT ""Group"".""Name"" as ""Group"",""Teacher"".""Surname"" as ""Teacher"", ""Room"" , ""Discipline"", ""Lessons"".""Id"" as ""Id""
				FROM schedule.""Lessons"" as ""Lessons""
				INNER JOIN schedule.""Teacher"" as ""Teacher"" on ""Lessons"".""TeacherId"" = ""Teacher"".""Id""
				INNER JOIN schedule.""Group"" as ""Group"" on ""Lessons"".""GroupId""=""Group"".""Id""
				WHERE ""Lessons"".""Id""='{id}';";
        var command = new CommandDefinition(query);
        var dbo = await _connection.QuerySingleAsync<LessonDbo>(command);
        _connection.Close();
        return dbo.ToDto();
    }

    public async Task<string> CreateLesson(Guid id, string groupId, string teacherId, string discipline, string inputDataId, int room)
    {
        if (_connection.State != ConnectionState.Open)
        {
            _connection.Open();
        }
        var query = @$"INSERT INTO schedule.""Lessons""(
            ""Id"", ""GroupId"", ""TeacherId"", ""Discipline"", inputdataid, ""Room"")
            VALUES ('{id}', '{groupId}', '{teacherId}', '{discipline}', '{inputDataId}', {room});";
        var command = new CommandDefinition(query);
        await _connection.ExecuteAsync(command);
        _connection.Close();
        return id.ToString();
    }
}