using Schedule.Application.DTOs;

namespace ScheduleSystem.Application.Handlers.ScheduleInputQueryHandler {
	public interface IScheduleInputQueryHandler {
		Task<ScheduleInputDto> Handle(string id);
	}
}