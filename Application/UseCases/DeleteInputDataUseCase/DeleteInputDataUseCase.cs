using ScheduleSystem.Infrastructure.ScheduleInputRepository;

class DeleteInputDataUseCase : IDeleteInputDataUseCase
{
    private readonly IScheduleInputRepository _scheduleInputRepository;

    public DeleteInputDataUseCase(IScheduleInputRepository scheduleInputRepository)
    {
        _scheduleInputRepository = scheduleInputRepository;
    }

    public async Task Execute(string id)
    {
        await _scheduleInputRepository.DeleteInputData(id);
    }
}