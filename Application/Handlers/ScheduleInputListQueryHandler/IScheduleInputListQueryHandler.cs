using Schedule.Application.DTOs;

namespace ScheduleSystem.Application.Handlers.ScheduleInputListQueryHandler {
	public interface IScheduleInputListQueryHandler {
		Task<IEnumerable<ScheduleInputDto>> Handle();
	}
}