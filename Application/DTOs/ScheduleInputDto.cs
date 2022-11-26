
using Newtonsoft.Json;

using ScheduleSystem.Application.DTOs;


namespace Schedule.Application.DTOs {
	public class ScheduleInputDto {
		public string Id { get; set; }
		public string Name { get; set; }
		public IEnumerable<LessonDto> Data { get; set; }

		[JsonConstructor]
		public ScheduleInputDto(string name, string id) {
			Name = name;
			//Data = JsonConvert.DeserializeObject<IEnumerable<LessonDto>>(data);
			Id = id;
		}

		public ScheduleInputDto(string name):this() {
			Name = name;
		}

		private ScheduleInputDto() {
			Id = Guid.NewGuid().ToString();
		}
	}
}
