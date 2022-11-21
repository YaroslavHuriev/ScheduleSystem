namespace ScheduleSystem.Domain.Entities {
	/// <summary>
	/// Фітнес функції
	/// </summary>
	static class FitnessFunctions {
		public static int GroupWindowPenalty = 10;//штраф за вікно в групи
		public static int TeacherWindowPenalty = 7;//штраф за вікно у викладача
		public static int LateLessonPenalty = 1;//штраф за пізню пару

		public static int LatestHour = 3;//максимальний час, коли зручно проводити пари

		/// <summary>
		/// Штраф за вікна
		/// </summary>
		public static int Windows(Plan plan) {
			var res = 0;

			for (byte day = 0; day < plan.DaysPerWeek; day++) {
				var groupHasLessons = new HashSet<string>();
				var teacherHasLessons = new HashSet<string>();

				for (byte hour = 0; hour < plan.HoursPerDay; hour++) foreach (var lesson in plan.HourPlans[day, hour].Lessons) {
						if (groupHasLessons.Contains(lesson.Group) && plan.GroupHasWindowOnThePreviousHour(day, hour, lesson.Group))
							res += GroupWindowPenalty;
						if (teacherHasLessons.Contains(lesson.Teacher) && plan.TeacherHasWindowOnThePreviousHour(day, hour, lesson.Teacher))
							res += TeacherWindowPenalty;

						groupHasLessons.Add(lesson.Group);
						teacherHasLessons.Add(lesson.Teacher);
					}
			}

			return res;
		}

		/// <summary>
		/// Штраф за пізні пари
		/// </summary>
		public static int LateLesson(Plan plan) {
			var res = 0;
			foreach (var pair in plan.GetLessons())
				if (pair.Hour > LatestHour)
					res += LateLessonPenalty;

			return res;
		}
	}
}
