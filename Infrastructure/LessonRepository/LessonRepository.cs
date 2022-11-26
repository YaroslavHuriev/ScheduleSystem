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
}