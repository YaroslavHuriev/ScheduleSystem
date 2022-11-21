namespace ScheduleSystem.Application.UseCases {
	public interface IGenerateScheduleUseCase {
		Task<string> Execute(string inputDataId);
	}
}