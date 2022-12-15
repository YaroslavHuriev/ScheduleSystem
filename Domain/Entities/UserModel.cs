using System.ComponentModel.DataAnnotations;
namespace ScheduleSystem.Domain.Entities
{
    public class UserModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}