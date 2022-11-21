using System.ComponentModel.DataAnnotations;

namespace ScheduleSystem.Common.Attributes {
	public class ValidGuid : ValidationAttribute {
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) {
			var guid = Convert.ToString(value);
			
			return Guid.TryParse(guid, out _)
				? ValidationResult.Success
				: new ValidationResult($"{validationContext.DisplayName} is invalid GUID.");
		}
	}
}
