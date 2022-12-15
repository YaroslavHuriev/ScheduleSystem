using Schedule.Application.DTOs;
using ScheduleSystem.Infrastructure.ScheduleInputRepository;

namespace ScheduleSystem.Application.UseCases.CreateInputDataUseCase
{
    public class CreateInputDataUseCase : ICreateInputDataUseCase {
		private readonly IScheduleInputRepository _scheduleInputRepository;

		public CreateInputDataUseCase(IScheduleInputRepository scheduleInputRepository) {
			_scheduleInputRepository = scheduleInputRepository;
		}

		public async Task<string> Execute(CreateInputDataUseCaseInput input) {
			var scheduleInputToCreate = new ScheduleInputDto(input.Name);
			await _scheduleInputRepository.CreateInputData(scheduleInputToCreate);
			return scheduleInputToCreate.Id;
		}
	}
}
