namespace ScheduleSystem.Domain.Entities {
	/// <summary>
	/// Генетичний алгоритм
	/// </summary>
	class Solver {
		public int MaxIterations = 1000;
		public int PopulationCount = 100;//повинно ділитись на 4

		public List<Func<Plan, SettingsDto, int>> FitnessFunctions = new List<Func<Plan, SettingsDto, int>>();

		public int Fitness(Plan plan, SettingsDto settings) {
			var res = 0;

			foreach (var f in FitnessFunctions)
				res += f(plan, settings);

			return res;
		}

		public Plan Solve(List<Lessоn> pairs, SettingsDto settings) {
			//створюємо популяцію
			var pop = new Population(pairs, settings.PopulationCount);
			if (pop.Count == 0)
				throw new Exception("Can not create any plan");
			//
			var count = settings.MaxIterations;
			while (count-- > 0) {
				//обраховуємо фітнес функцію для всіх планів
				pop.ForEach(p => p.FitnessValue = Fitness(p, settings));
				//сортуємо популяцію за фітнес функцією
				pop.Sort((p1, p2) => p1.FitnessValue.CompareTo(p2.FitnessValue));
				//знайдений ідеальний план?
				if (pop[0].FitnessValue == 0)
					return pop[0];
				//відбираємо 25% найкращих планів
				pop.RemoveRange(pop.Count / 4, pop.Count - pop.Count / 4);
				//від кожного предка створюємо трьох нащадків з мутаціями
				var c = pop.Count;
				for (int i = 0; i < c; i++) {
					pop.AddChildOfParent(pop[i]);
					pop.AddChildOfParent(pop[i]);
					pop.AddChildOfParent(pop[i]);
				}
			}

			//обраховуємо фітнес функцію для всіх планів
			pop.ForEach(p => p.FitnessValue = Fitness(p, settings));
			//сортуємо популяцію за фітнес функцією
			pop.Sort((p1, p2) => p1.FitnessValue.CompareTo(p2.FitnessValue));

			//повертаємо найкращий план
			return pop[0];
		}
	}
}
