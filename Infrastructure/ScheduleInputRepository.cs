using System.Data;

using Dapper;

using MongoDB.Driver;

using Schedule.Application.DTOs;

using ScheduleSystem.Infrastructure.Options;

namespace ScheduleSystem.Infrastructure {
	public class ScheduleInputRepository : IScheduleInputRepository {
		private MongoClient _mongoClient;
		private IMongoDatabase _db;
		private IMongoCollection<ScheduleInputDbo> _collection;
		private readonly IDbConnection _connection;
		public ScheduleInputRepository(MongoOptions options, IDbConnection connection) {
			_mongoClient = new MongoClient(options.ConnectionString);
			_db = _mongoClient.GetDatabase(options.ScheduleDbName);
			_collection = _db.GetCollection<ScheduleInputDbo>(options.ScheduleInputCollectionName);
			_connection = connection;
		}

		public async Task CreateInputData(ScheduleInputDto document) {
			//await _collection.InsertOneAsync(new ScheduleInputDbo(document));
		}

		public async Task<IEnumerable<ScheduleInputDto>> GetInputDatas() {
			if (_connection.State != ConnectionState.Open) {
				_connection.Open();
			}
			var query = @"SELECT json_agg(json_build_object('Group',""Group"".""Name"",'Teacher', ""Teacher"".""Surname"",'Room', ""Room"",'Discipline', ""Discipline"")) as ""Data"", ""InputData"".""Name"" as ""Name"", ""InputData"".""Id"" as ""Id""
	FROM schedule.""Lessons"" as ""Lessons"" inner join schedule.""Teacher"" as ""Teacher""
	on ""Lessons"".""TeacherId"" = ""Teacher"".""Id"" inner join schedule.""Group"" as ""Group"" on 
	""Lessons"".""GroupId""=""Group"".""Id"" inner join schedule.""InputData"" as ""InputData"" on ""Lessons"".""inputdataid""=""InputData"".""Id""
	Group by ""InputData"".""Name"",""InputData"".""Id"";";
			//		var query = @"SELECT ""Group"".""Name"" as ""Group"", ""Teacher"".""Surname"" as ""Teacher"", ""Room"", ""Discipline""
			//FROM schedule.""Lessons"" as ""Lessons"" inner join schedule.""Teacher"" as ""Teacher""
			//on ""Lessons"".""TeacherId"" = ""Teacher"".""Id"" inner join schedule.""Group"" as ""Group"" on 
			//""Lessons"".""GroupId""=""Group"".""Id"";";
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
