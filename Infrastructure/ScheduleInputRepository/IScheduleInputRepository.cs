using Schedule.Application.DTOs;

namespace ScheduleSystem.Infrastructure.ScheduleInputRepository
{
    public interface IScheduleInputRepository
    {
        Task CreateInputData(ScheduleInputDto document);
        Task DeleteInputData(string id);
        Task<ScheduleInputDto> GetById(string id);
        Task<IEnumerable<ScheduleInputDto>> GetInputDatas();
    }
}