using ScheduleSystem.Application.DTOs;

namespace ScheduleSystem.Infrastructure.ScheduleRepository {
	public interface IScheduleRepository {
		Task CreateSchedule(ScheduleDto schedule);
		Task<IEnumerable<ScheduleDto>> GetScheduleList();
	}
}