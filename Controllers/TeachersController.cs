namespace ScheduleSystem.Controllers
{

    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly IGetTeachersListQueryHandler _getTeachersListQueryHandler;

        public TeachersController(IGetTeachersListQueryHandler getTeachersListQueryHandler)
        {
            _getTeachersListQueryHandler = getTeachersListQueryHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetTeachersList([FromQuery] string? searchString = null)
        {
            var result = await _getTeachersListQueryHandler.Handle(searchString);
            return Ok(result);
        }
    }
}