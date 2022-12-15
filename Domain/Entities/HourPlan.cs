
namespace ScheduleSystem.Domain.Entities {
	/// <summary>
	/// План на годину
	/// </summary>
	class HourPlan {
		public List<Lessоn> Lessons { get; private set; } = new List<Lessоn>();
		public bool AddLesson(Lessоn lesson) {
			if (!LessonCanBeAddedToTheTime(lesson))
				return false;//в цей час уже є пара у викладача або групи

			Lessons.Add(lesson);

			return true;
		}

		public void RemoveLesson(string teacher) {
			Lessons = Lessons.Where(l => l.Teacher != teacher).ToList();
		}

		private bool LessonCanBeAddedToTheTime(Lessоn lesson) {
			if (Lessons?.Any(existingLesson => existingLesson.Group == lesson.Group ||
			existingLesson.Teacher == lesson.Teacher ||
			existingLesson.Room == lesson.Room) is true) return false;
			return true;
		}

		public HourPlan Clone() {
			var res = new HourPlan();
			res.Lessons = Lessons.Select(l => new Lessоn(l.Day, l.Hour, l.Group, l.Teacher, l.Room, l.Discipline, l.Id)).ToList();

			return res;
		}
	}
}
