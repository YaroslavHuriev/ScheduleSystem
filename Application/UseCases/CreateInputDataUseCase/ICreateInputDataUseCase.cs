namespace ScheduleSystem.Application.UseCases.CreateInputDataUseCase {
	public interface ICreateInputDataUseCase {
		Task<string> Execute(CreateInputDataUseCaseInput input);
	}
}