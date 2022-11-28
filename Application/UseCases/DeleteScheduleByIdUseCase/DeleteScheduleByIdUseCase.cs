using ScheduleSystem.Infrastructure.ScheduleRepository;

public class DeleteScheduleByIdUseCase : IDeleteScheduleByIdUseCase
{
    private readonly IScheduleRepository _scheduleRepository;

    public DeleteScheduleByIdUseCase(IScheduleRepository scheduleRepository)
    {
        _scheduleRepository = scheduleRepository;
    }

    public async Task Execute(string id)
    {
        await _scheduleRepository.DeleteScheduleById(id);
    }
}