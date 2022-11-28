using System.Data;

using Dapper;

using ScheduleSystem.Application.DTOs;

namespace ScheduleSystem.Infrastructure.ScheduleRepository {
	public class ScheduleRepository : IScheduleRepository {
		private readonly IDbConnection _connection;

		public ScheduleRepository(IDbConnection connection) {
			_connection = connection;
		}

		public async Task CreateSchedule(ScheduleDto schedule) {
			if (_connection.State != ConnectionState.Open) {
				_connection.Open();
			}
			var query = @$"INSERT INTO schedule.""Schedule""(
				""Id"", ""Name"")
				VALUES ('{schedule.Id}', '{schedule.Name}');";
			var command = new CommandDefinition(query);
			await _connection.ExecuteAsync(command);
			_connection.Close();
		}

		public async Task<IEnumerable<ScheduleDto>> GetScheduleList() {
			if (_connection.State != ConnectionState.Open) {
				_connection.Open();
			}
			var query = @$"SELECT ""Id"", ""Name""
				FROM schedule.""Schedule"";";
			var command = new CommandDefinition(query);
			var dbos = await _connection.QueryAsync<ScheduleDbo>(command);
			_connection.Close();
			return dbos.Select(dbo => dbo.ToDto());
		}

		public async Task DeleteScheduleById(string id) {
			if (_connection.State != ConnectionState.Open) {
				_connection.Open();
			}
			var query = @$"DELETE FROM schedule.""Schedule""
				WHERE ""Schedule"".""Id""='{id}';";
			var command = new CommandDefinition(query);
			var dbos = await _connection.ExecuteAsync(command);
			_connection.Close();
		}
	}
}
