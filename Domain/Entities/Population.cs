namespace ScheduleSystem.Domain.Entities {
	/// <summary>
	/// Популяція планів
	/// </summary>
	class Population : List<Plan> {
		public Population(List<Lessоn> pairs, int count) {
			var maxIterations = count * 2;

			do {
				var plan = new Plan();
				if (plan.Init(pairs))
					Add(plan);
			} while (maxIterations-- > 0 && Count < count);
		}

		public bool AddChildOfParent(Plan parent) {
			int maxIterations = 10;

			do {
				var plan = new Plan();
				if (plan.Init(parent)) {
					Add(plan);
					return true;
				}
			} while (maxIterations-- > 0);
			return false;
		}
	}
}
