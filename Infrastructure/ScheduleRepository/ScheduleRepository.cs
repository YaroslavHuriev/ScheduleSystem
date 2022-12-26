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
				VALUES (@Id, @Name);";
			var command = new CommandDefinition(query, new {Id = Guid.Parse(schedule.Id), Name = schedule.Name});
			await _connection.ExecuteAsync(command);
			_connection.Close();
		}

		public async Task<IEnumerable<ScheduleDto>> GetScheduleList(bool currentSchedule = false) {
			if (_connection.State != ConnectionState.Open) {
				_connection.Open();
			}
			var query = @$"SELECT ""Id"", ""Name""
				FROM schedule.""Schedule""";
			if(currentSchedule){
				query += @"WHERE schedule.""Schedule"".""IsCurrent""=True";
			}
			query+=";";
			var command = new CommandDefinition(query);
			var dbos = await _connection.QueryAsync<ScheduleDbo>(command);
			_connection.Close();
			return dbos.Select(dbo => dbo.ToDto());
		}

		public async Task<ScheduleDto> GetScheduleById(string id) {
			if (_connection.State != ConnectionState.Open) {
				_connection.Open();
			}
			var query = @$"SELECT ""Id"", ""Name"", ""IsCurrent""
				FROM schedule.""Schedule""
				WHERE schedule.""Schedule"".""Id""=@Id;";
			var command = new CommandDefinition(query, new{Id = Guid.Parse(id)});
			var dbo = await _connection.QuerySingleOrDefaultAsync<ScheduleDbo>(command);
			_connection.Close();
			return dbo.ToDto();
		}

		public async Task DeleteScheduleById(string id) {
			if (_connection.State != ConnectionState.Open) {
				_connection.Open();
			}
			var query = @$"DELETE FROM schedule.""Schedule""
				WHERE ""Schedule"".""Id""=@Id;";
			var command = new CommandDefinition(query, new{Id=Guid.Parse(id)});
			var dbos = await _connection.ExecuteAsync(command);
			_connection.Close();
		}

		public async Task MakeScheduleCurrentById(string id) {
			if (_connection.State != ConnectionState.Open) {
				_connection.Open();
			}
			var query = @"UPDATE schedule.""Schedule""
				SET ""IsCurrent""=False
				WHERE schedule.""Schedule"".""IsCurrent""!=False;
				UPDATE schedule.""Schedule""
				SET ""IsCurrent""=True
				WHERE schedule.""Schedule"".""Id""=@Id;";
			var command = new CommandDefinition(query, new {Id = Guid.Parse(id)});
			await _connection.ExecuteAsync(command);
			_connection.Close();
		}
	}
}
