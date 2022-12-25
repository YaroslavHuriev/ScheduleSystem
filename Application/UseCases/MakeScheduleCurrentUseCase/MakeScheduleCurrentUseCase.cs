using ScheduleSystem.Infrastructure.ScheduleRepository;

public class MakeScheduleCurrentUseCase : IMakeScheduleCurrentUseCase
{
    private readonly IScheduleRepository _scheduleRepository;

    public MakeScheduleCurrentUseCase(IScheduleRepository scheduleRepository)
    {
        _scheduleRepository = scheduleRepository;
    }

    public async Task Execute(string id)
    {
        await _scheduleRepository.MakeScheduleCurrentById(id);
    }
}