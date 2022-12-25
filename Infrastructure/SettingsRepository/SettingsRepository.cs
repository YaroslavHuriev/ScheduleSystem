using System.Data;
using Dapper;

public class SettingsRepository : ISettingsRepository
{
    private readonly IDbConnection _connection;

    public SettingsRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<SettingsDto> GetSettings()
    {
        if (_connection.State != ConnectionState.Open)
        {
            _connection.Open();
        }
        var query = @"SELECT ""Id"", ""MaxIterations"", ""PopulationCount"", ""LatestHour"", ""MaxOccurrencesOfOneDisciplinePerDayForGroup"", ""GroupWindowPenalty"", ""TeacherWindowPenalty"", ""LateLessonPenalty"", ""TooMuchOccurrencesOfOneDisciplinePerDayPenalty""
	        FROM schedule.""ScheduleGenerationSettings""
            WHERE schedule.""ScheduleGenerationSettings"".""Id""='29dcffa2-ec84-4b5c-b43e-a304751f777b';";
        var command = new CommandDefinition(query);
        var dbo = await _connection.QuerySingleAsync<SettingsDbo>(command);
        _connection.Close();
        return dbo.ToDto();
    }

    public async Task UpdateSettings(int maxIterations,
     int populationCount,
     int latestHour,
     int maxOccurrencesOfOneDisciplinePerDayForGroup,
     int groupWindowPenalty,
     int teacherWindowPenalty,
     int lateLessonPenalty,
     int tooMuchOccurrencesOfOneDisciplinePerDayPenalty
     )
    {
        if (_connection.State != ConnectionState.Open)
        {
            _connection.Open();
        }
        var query = @"UPDATE schedule.""ScheduleGenerationSettings""
	SET ""MaxIterations""=@MaxIterations,
     ""PopulationCount""=@PopulationCount,
     ""LatestHour""=@LatestHour,
     ""MaxOccurrencesOfOneDisciplinePerDayForGroup""=@MaxOccurrencesOfOneDisciplinePerDayForGroup,
     ""GroupWindowPenalty""=@GroupWindowPenalty,
     ""TeacherWindowPenalty""=@TeacherWindowPenalty,
     ""LateLessonPenalty""=@LateLessonPenalty,
     ""TooMuchOccurrencesOfOneDisciplinePerDayPenalty""=@TooMuchOccurrencesOfOneDisciplinePerDayPenalty
	WHERE schedule.""ScheduleGenerationSettings"".""Id""='29dcffa2-ec84-4b5c-b43e-a304751f777b';";
        var command = new CommandDefinition(query, new
        {
            MaxIterations = maxIterations,
            PopulationCount = populationCount,
            LatestHour = latestHour,
            MaxOccurrencesOfOneDisciplinePerDayForGroup = maxOccurrencesOfOneDisciplinePerDayForGroup,
            GroupWindowPenalty = groupWindowPenalty,
            TeacherWindowPenalty = teacherWindowPenalty,
            LateLessonPenalty = lateLessonPenalty,
            TooMuchOccurrencesOfOneDisciplinePerDayPenalty = tooMuchOccurrencesOfOneDisciplinePerDayPenalty
        });
        await _connection.ExecuteAsync(command);
        _connection.Close();
    }
}