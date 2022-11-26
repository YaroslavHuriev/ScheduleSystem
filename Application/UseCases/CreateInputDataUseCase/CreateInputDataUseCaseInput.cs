namespace ScheduleSystem.Application.UseCases.CreateInputDataUseCase {
	public class CreateInputDataUseCaseInput {
		public string Name { get; }

		public CreateInputDataUseCaseInput(string name) {
			Name = name;
		}
	}
}