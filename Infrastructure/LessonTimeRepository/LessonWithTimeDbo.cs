using ScheduleSystem.Application.DTOs;

namespace ScheduleSystem.Infrastructure.LessonTimeRepository {
	public class LessonWithTimeDbo {
		public Guid Id { get; set; }
		public string Group { get; set; }
		public string Teacher { get; set; }
		public int Room { get; set; }
		public string Discipline { get; set; }
		public byte Day { get; set; }
		public byte Hour { get; set; }

		public LessonWithTimeDto ToDto() {
			return new LessonWithTimeDto {
				Id = Id.ToString(),
				Group = Group,
				Teacher = Teacher,
				Room = Room,
				Discipline = Discipline,
				Day = Day,
				Hour = Hour
			};
		}
	}
}
