namespace ScheduleSystem.Domain.Entities {
	/// <summary>
	/// Пара
	/// </summary>
	class Lessоn {
		public byte Day { get; set; } = 255;
		public byte Hour { get; set; } = 255;
		public string Group { get; set; }
		public string Teacher { get; set; }
		public int Room { get; set; }
		public string Discipline { get; set; }
		public string Id { get; set; }

		public Lessоn(byte day, byte hour, string group, string teacher, int room, string discipline, string id)
			: this(group, teacher, room, discipline, id) {
			Day = day;
			Hour = hour;
		}

		public Lessоn(string group, string teacher, int room, string discipline, string id) {
			Group = group;
			Teacher = teacher;
			Room = room;
			Discipline = discipline;
			Id = id;
		}
	}
}
