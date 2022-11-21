using System.Text;

namespace ScheduleSystem.Domain.Entities {
	/// <summary>
	/// План занять
	/// </summary>
	class Plan {
		private int daysPerWeek = 5;//5 навчальних днів на тиждень
		private int hoursPerDay = 6;//до 6 пар на день

		private static Random rnd = new Random(3);

		/// <summary>
		/// План по днях (перший індекс) і годинам (другий індекс)
		/// </summary>
		public HourPlan[,] HourPlans { get; set; }

		public int FitnessValue { get; internal set; }
		public int DaysPerWeek { get => daysPerWeek; set => daysPerWeek = value; }
		public int HoursPerDay { get => hoursPerDay; set => hoursPerDay = value; }

		public Plan() {
			HourPlans = new HourPlan[DaysPerWeek, HoursPerDay];
		}

		public bool AddLesson(Lessоn les) {
			return HourPlans[les.Day, les.Hour].AddLesson(les);
		}

		public void RemoveLesson(Lessоn les) {
			HourPlans[les.Day, les.Hour].RemoveLesson(les.Teacher);
		}

		/// <summary>
		/// Додати групу з викладачем на будь який день і будь який час
		/// </summary>
		public bool AddToAnyDayAndHour(string group, string teacher, int room, string discipline, string id) {
			int maxIterations = 30;
			do {
				var day = (byte)rnd.Next(DaysPerWeek);
				if (AddToAnyHour(day, group, teacher, room, discipline, id))
					return true;
			} while (maxIterations-- > 0);

			return false;//не змогли нікуди додати
		}

		/// <summary>
		/// Додати групу з викладачем на будь який час
		/// </summary>
		bool AddToAnyHour(byte day, string group, string teacher, int room, string discipline, string id) {
			for (byte hour = 0; hour < HoursPerDay; hour++) {
				var les = new Lessоn(day, hour, group, teacher, room, discipline, id);
				if (AddLesson(les))
					return true;
			}

			return false;//немає вільних годин у цей день
		}

		public bool TeacherHasWindowOnThePreviousHour(byte day, byte hour, string teacher) {
			return !HourPlans[day, hour - 1].Lessons.Any(l => l.Teacher == teacher);
		}

		public bool GroupHasWindowOnThePreviousHour(byte day, byte hour, string group) {
			return !HourPlans[day, hour - 1].Lessons.Any(l => l.Group == group);
		}

		/// <summary>
		/// Створення плану за списком пар
		/// </summary>
		public bool Init(List<Lessоn> pairs) {
			for (int i = 0; i < HoursPerDay; i++)
				for (int j = 0; j < DaysPerWeek; j++)
					HourPlans[j, i] = new HourPlan();

			foreach (var p in pairs)
				if (!AddToAnyDayAndHour(p.Group, p.Teacher, p.Room, p.Discipline, p.Id))
					return false;
			return true;
		}

		/// <summary>
		/// Створення нащадка з мутацією
		/// </summary>
		public bool Init(Plan parent) {
			//копіюємо предка
			for (int i = 0; i < HoursPerDay; i++)
				for (int j = 0; j < DaysPerWeek; j++)
					HourPlans[j, i] = parent.HourPlans[j, i].Clone();

			//обираємо два випадкових дня
			var day1 = (byte)rnd.Next(DaysPerWeek);
			var day2 = (byte)rnd.Next(DaysPerWeek);

			//знаходимо пари в ці дні
			var pairs1 = GetLessonsOfDay(day1).ToList();
			var pairs2 = GetLessonsOfDay(day2).ToList();

			//обираємо випадкові пари
			if (pairs1.Count == 0 || pairs2.Count == 0) return false;
			var pair1 = pairs1[rnd.Next(pairs1.Count)];
			var pair2 = pairs2[rnd.Next(pairs2.Count)];

			//створюємо мутацію - змінюємо місцями дві випадкові пари
			RemoveLesson(pair1);//видаляємо
			RemoveLesson(pair2);//видаляємо
			var res1 = AddToAnyHour(pair2.Day, pair1.Group, pair1.Teacher, pair1.Room, pair1.Discipline, pair1.Id);//вставляємо на випадкове місце
			var res2 = AddToAnyHour(pair1.Day, pair2.Group, pair2.Teacher, pair2.Room, pair2.Discipline, pair2.Id);//вставляємо на випадкове місце
			return res1 && res2;
		}

		public IEnumerable<Lessоn> GetLessonsOfDay(byte day) {
			List<Lessоn> lessons = new List<Lessоn>();
			for (byte hour = 0; hour < HoursPerDay; hour++)
				lessons.AddRange(HourPlans[day, hour].Lessons);
			return lessons;
		}

		public IEnumerable<Lessоn> GetLessons() {
			List<Lessоn> lessons = new List<Lessоn>();
			for (byte day = 0; day < DaysPerWeek; day++)
				for (byte hour = 0; hour < HoursPerDay; hour++)
					lessons.AddRange(HourPlans[day, hour].Lessons);
			return lessons;
		}

		public override string ToString() {
			var sb = new StringBuilder();
			for (byte day = 0; day < DaysPerWeek; day++) {
				sb.AppendFormat("Day {0}\r\n", day);
				for (byte hour = 0; hour < HoursPerDay; hour++) {
					sb.AppendFormat("Hour {0}: ", hour);
					foreach (var p in HourPlans[day, hour].Lessons)
						sb.AppendFormat("Gr-Tch: {0}-{1} ", p.Group, p.Teacher);
					sb.AppendLine();
				}
			}

			sb.AppendFormat("Fitness: {0}\r\n", FitnessValue);

			return sb.ToString();
		}
	}
}
