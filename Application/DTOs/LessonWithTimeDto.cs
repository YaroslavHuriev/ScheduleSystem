namespace ScheduleSystem.Application.DTOs {
	public class LessonWithTimeDto {
		public string Id { get; set; }
		public string Group { get; set; }
		public string Teacher { get; set; }
		public int Room { get; set; }
		public string Discipline { get; set; }
		public byte Day { get; set; }
		public byte Hour { get; set; }
	}
}
