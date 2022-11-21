using Schedule.Application.DTOs;

namespace ScheduleSystem.Infrastructure {
	public class ScheduleInputDbo {
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Data { get; set; }

		private ScheduleInputDbo() {
			Id = Guid.NewGuid();
		}

		//public ScheduleInputDbo(ScheduleInputDto input) : this()
		//{
		//	Name = input.Name;
		//	Data = input.Data;
		//}

		public ScheduleInputDto ToDto() {
			return new ScheduleInputDto(Name, Data, Id.ToString());
		}
	}
}
