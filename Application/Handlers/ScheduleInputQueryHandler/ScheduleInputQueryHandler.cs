using Schedule.Application.DTOs;
using ScheduleSystem.Infrastructure.ScheduleInputRepository;

namespace ScheduleSystem.Application.Handlers.ScheduleInputQueryHandler
{
    public class ScheduleInputQueryHandler : IScheduleInputQueryHandler {
		private readonly IScheduleInputRepository _scheduleInputRepository;
		public ScheduleInputQueryHandler(IScheduleInputRepository scheduleInputRepository) {
			_scheduleInputRepository = scheduleInputRepository;
		}

		public async Task<ScheduleInputDto> Handle(string id) {
			return await _scheduleInputRepository.GetById(id);
		}
	}
}
