using System.ComponentModel.DataAnnotations;

namespace ScheduleSystem.Controllers.Requests {
	public class CreateScheduleInputRequest {
		[Required(AllowEmptyStrings = false)]
		public string Name { get; set; }
	}
}
