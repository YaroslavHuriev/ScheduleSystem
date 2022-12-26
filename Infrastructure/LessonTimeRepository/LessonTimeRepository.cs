using System.Data;

using Dapper;
using ScheduleSystem.Application.DTOs;

namespace ScheduleSystem.Infrastructure.LessonTimeRepository {
	public class LessonTimeRepository : ILessonTimeRepository {
		private readonly IDbConnection _connection;

		public LessonTimeRepository(IDbConnection connection) {
			_connection = connection;
		}

		public async Task CreateLessonsWithTimes(string scheduleId, IEnumerable<LessonTimeDto> lessons) {
			if (_connection.State != ConnectionState.Open) {
				_connection.Open();
			}
			var query = @$"INSERT INTO schedule.""LessonTime""(
				""Id"", ""LessonId"", ""Day"", ""Hour"", ""ScheduleId"")
				VALUES {string.Join(',', lessons.Select(l => $"('{l.Id}', '{l.LessonId}', {l.Day}, {l.Hour}, '{scheduleId}')"))};";
			var command = new CommandDefinition(query);
			await _connection.ExecuteAsync(command);
			_connection.Close();
		}

		public async Task<IEnumerable<LessonWithTimeDto>> GetLessonsByScheduleId(string scheduleId) {
			if (_connection.State != ConnectionState.Open) {
				_connection.Open();
			}
			var query = @$"SELECT lt.""Id"" as ""Id"", ""g"".""Name"" as ""Group"", CONCAT(""t"".""Surname"",' ',""t"".""FirstName"") as ""Teacher"", l.""Discipline"", l.""Room"", lt.""Day"", lt.""Hour""
				FROM schedule.""Lessons"" as l
				inner join schedule.""LessonTime"" as lt
				on l.""Id""=lt.""LessonId""
				inner join schedule.""Group"" as ""g""
				on l.""GroupId""=""g"".""Id""
				inner join schedule.""Teacher"" as ""t""
				on l.""TeacherId""=""t"".""Id""
				WHERE lt.""ScheduleId""=@ScheduleId;";
			var command = new CommandDefinition(query, new{ScheduleId=Guid.Parse(scheduleId)});
			var dbos = await _connection.QueryAsync<LessonWithTimeDbo>(command);
			return dbos.Select(dbo => dbo.ToDto());
		}
	}
}
