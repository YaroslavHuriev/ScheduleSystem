namespace ScheduleSystem.Controllers
{

    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGetGroupsListQueryHandler _getGroupsListQueryHandler;

        public GroupsController(IGetGroupsListQueryHandler getGroupsListQueryHandler)
        {
            _getGroupsListQueryHandler = getGroupsListQueryHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetGroupsList([FromQuery] string? searchString = null)
        {
            var result = await _getGroupsListQueryHandler.Handle(searchString);
            return Ok(result);
        }
    }
}