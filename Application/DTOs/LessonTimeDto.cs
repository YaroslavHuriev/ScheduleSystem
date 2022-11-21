namespace ScheduleSystem.Application.DTOs {
	public class LessonTimeDto{
		public string Id { get; set; }
		public byte Day { get; set; }
		public byte Hour { get; set; }
		public string LessonId { get; set; }


		public LessonTimeDto(string id, byte day, byte hour, string lessonId) {
			Id = id;
			Day = day;
			Hour = hour;
			LessonId = lessonId;
		}
	}
}
