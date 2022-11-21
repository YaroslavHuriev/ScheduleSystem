using System.ComponentModel.DataAnnotations;

using ScheduleSystem.Common.Attributes;

namespace ScheduleSystem.Controllers.Requests {
	public class GenerateScheduleRequest {
		[ValidGuid, Required]
		public string InputDataId { get; set; }

	}
}
