namespace ScheduleSystem.Infrastructure.Options {
	public class MongoOptions {
		public string ConnectionString { get; }
		public string ScheduleDbName { get; }
		public string ScheduleInputCollectionName { get; }

		public MongoOptions(string connectionString, string scheduleDbName, string scheduleInputCollectionName) {
			if (string.IsNullOrEmpty(connectionString)) {
				throw new ArgumentNullException(nameof(connectionString));
			}
			if (string.IsNullOrEmpty(scheduleDbName)) {
				throw new ArgumentNullException(nameof(scheduleDbName));
			}
			if (string.IsNullOrEmpty(scheduleInputCollectionName)) {
				throw new ArgumentNullException(nameof(scheduleInputCollectionName));
			}
			ConnectionString = connectionString;
			ScheduleDbName = scheduleDbName;
			ScheduleInputCollectionName = scheduleInputCollectionName;
		}
	}
}
