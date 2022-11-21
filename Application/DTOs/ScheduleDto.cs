namespace ScheduleSystem.Application.DTOs {
	public class ScheduleDto {
		public string Id { get; set; }
		public string Name { get; set; }
		public IEnumerable<LessonTimeDto> Lessons { get; set; }

		public ScheduleDto(string id, string name, IEnumerable<LessonTimeDto> lessons) {
			Id = id;
			Name = name;
			Lessons = lessons;
		}
	}
}
