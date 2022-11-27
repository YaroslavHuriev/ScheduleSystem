using ScheduleSystem.Application.DTOs;

namespace ScheduleSystem.Infrastructure.ScheduleRepository
{
    public class ScheduleDbo
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ScheduleDto ToDto()
        {
            return new ScheduleDto
            {
                Id = Id.ToString(),
                Name = Name
            };
        }
    }
}