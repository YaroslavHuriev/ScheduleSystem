using ScheduleSystem.Application.DTOs;

namespace ScheduleSystem.Infrastructure.ScheduleRepository {
	public interface IScheduleRepository {
		Task CreateSchedule(ScheduleDto schedule);
		Task<IEnumerable<ScheduleDto>> GetScheduleList(bool currentSchedule = false);
		Task DeleteScheduleById(string id);
		Task MakeScheduleCurrentById(string id);
		Task<ScheduleDto> GetScheduleById(string id);
	}
}