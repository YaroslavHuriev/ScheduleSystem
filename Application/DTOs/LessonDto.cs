namespace ScheduleSystem.Application.DTOs {
	public class LessonDto {
		public string Id { get; set; }
		public string Group { get; set; }
		public string Teacher { get; set; }
		public int Room { get; set; }
		public string Discipline { get; set; }

		public LessonDto(string group, string teacher, int room, string discipline, string id) {
			Group = group;
			Teacher = teacher;
			Room = room;
			Discipline = discipline;
			Id = id;
		}
	}
}
