using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ScheduleSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {

        private readonly ILessonRepository _lessonRepository;
        public LessonsController(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery, Required] string inputDataId)
        {
            var result = await _lessonRepository.GetLessonsByInputDataId(inputDataId);
            return new OkObjectResult(result);
        }
    }
}
