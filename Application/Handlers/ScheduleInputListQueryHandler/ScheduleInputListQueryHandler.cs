using Schedule.Application.DTOs;

using ScheduleSystem.Infrastructure;

namespace ScheduleSystem.Application.Handlers.ScheduleInputListQueryHandler {
	public class ScheduleInputListQueryHandler : IScheduleInputListQueryHandler {

		private readonly IScheduleInputRepository _scheduleInputRepository;
		public ScheduleInputListQueryHandler(IScheduleInputRepository scheduleInputRepository) {
			_scheduleInputRepository = scheduleInputRepository;
		}

		public async Task<IEnumerable<ScheduleInputDto>> Handle() {
			return await _scheduleInputRepository.GetInputDatas();
		}
	}
}
