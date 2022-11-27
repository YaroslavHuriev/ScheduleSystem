using ScheduleSystem.Application.DTOs;

public interface IScheduleListQueryHandler
{
    Task<IEnumerable<ScheduleDto>> Handle();
}
