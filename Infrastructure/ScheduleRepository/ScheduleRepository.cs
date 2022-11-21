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
	}
}
