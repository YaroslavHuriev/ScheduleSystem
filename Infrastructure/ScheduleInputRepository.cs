using System.Data;

using Dapper;

using Schedule.Application.DTOs;


namespace ScheduleSystem.Infrastructure {
	public class ScheduleInputRepository : IScheduleInputRepository {
		private readonly IDbConnection _connection;
		public ScheduleInputRepository(IDbConnection connection) {
			_connection = connection;
		}

		public async Task CreateInputData(ScheduleInputDto document) {
			if (_connection.State != ConnectionState.Open) {
				_connection.Open();
			}
			var query = @$"INSERT INTO schedule.""InputData""(
				""Id"", ""Name"")
				VALUES ('{document.Id}', '{document.Name}');";
			var command = new CommandDefinition(query);
			await _connection.ExecuteAsync(command);
			_connection.Close();
		}

		public async Task DeleteInputData(string id) {
			if (_connection.State != ConnectionState.Open) {
				_connection.Open();
			}
			var query = @$"DELETE FROM schedule.""InputData"" AS ""InputData""
				WHERE ""InputData"".""Id""='{id}';";
			var command = new CommandDefinition(query);
			await _connection.ExecuteAsync(command);
			_connection.Close();
		}

		public async Task<IEnumerable<ScheduleInputDto>> GetInputDatas() {
			if (_connection.State != ConnectionState.Open) {
				_connection.Open();
			}
			var query = @"SELECT ""InputData"".""Name"" as ""Name"", ""InputData"".""Id"" as ""Id""
	FROM schedule.""InputData"" as ""InputData"";";
			var command = new CommandDefinition(query);
			var dbos = await _connection.QueryAsync<ScheduleInputDbo>(command);
			_connection.Close();
			return dbos.Select(dbo => dbo.ToDto());
		}

		public async Task<ScheduleInputDto> GetById(string id) {
			if (_connection.State != ConnectionState.Open) {
				_connection.Open();
			}
			var query = @$"SELECT json_agg(json_build_object('Group',""Group"".""Name"",'Teacher', ""Teacher"".""Surname"",'Room', ""Room"",'Discipline', ""Discipline"", 'Id', ""Lessons"".""Id"")) as ""Data"",
				""InputData"".""Name"" as ""Name"",
				""InputData"".""Id"" as ""Id""
				FROM schedule.""Lessons"" as ""Lessons""
				INNER JOIN schedule.""Teacher"" as ""Teacher"" on ""Lessons"".""TeacherId"" = ""Teacher"".""Id""
				INNER JOIN schedule.""Group"" as ""Group"" on ""Lessons"".""GroupId""=""Group"".""Id""
				INNER JOIN schedule.""InputData"" as ""InputData"" on ""Lessons"".""inputdataid""=""InputData"".""Id""
				WHERE ""InputData"".""Id""='{id}'	
				GROUP BY ""InputData"".""Name"",""InputData"".""Id"";";
			var command = new CommandDefinition(query);
			var dbo = await _connection.QuerySingleAsync<ScheduleInputDbo>(command);
			_connection.Close();
			return dbo.ToDto();
		}
	}
}
