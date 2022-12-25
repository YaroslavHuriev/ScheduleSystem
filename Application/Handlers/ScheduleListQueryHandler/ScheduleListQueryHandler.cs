using ScheduleSystem.Application.DTOs;
using ScheduleSystem.Infrastructure.ScheduleRepository;

public class ScheduleListQueryHandler : IScheduleListQueryHandler
{
    private readonly IScheduleRepository _scheduleRepository;

    public ScheduleListQueryHandler(IScheduleRepository scheduleRepository)
    {
        _scheduleRepository = scheduleRepository;
    }

    public async Task<IEnumerable<ScheduleDto>> Handle(bool currentSchedule = false)
    {
        return await _scheduleRepository.GetScheduleList(currentSchedule);
    }
}