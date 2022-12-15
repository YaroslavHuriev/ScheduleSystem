using Schedule.Application.DTOs;

namespace ScheduleSystem.Infrastructure.ScheduleInputRepository
{
    public class ScheduleInputDbo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        private ScheduleInputDbo()
        {
            Id = Guid.NewGuid();
        }
        public ScheduleInputDto ToDto()
        {
            return new ScheduleInputDto(Name, Id.ToString());
        }
    }
}
