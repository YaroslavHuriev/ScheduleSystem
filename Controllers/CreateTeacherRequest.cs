using System.ComponentModel.DataAnnotations;

namespace ScheduleSystem.Controllers
{
    public class CreateTeacherRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Surname { get; set; }
    }
}