namespace ScheduleSystem.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ScheduleSystem.Common.Attributes;

    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGetGroupsListQueryHandler _getGroupsListQueryHandler;
        private readonly IDeleteGroupUseCase _deleteGroup;
        private readonly ICreateGroupUseCase _createGroup;

        public GroupsController(IGetGroupsListQueryHandler getGroupsListQueryHandler, ICreateGroupUseCase createGroup, IDeleteGroupUseCase deleteGroup)
        {
            _getGroupsListQueryHandler = getGroupsListQueryHandler;
            _createGroup = createGroup;
            _deleteGroup = deleteGroup;
        }

        [HttpGet]
        public async Task<IActionResult> GetGroupsList([FromQuery] string? searchString = null)
        {
            var result = await _getGroupsListQueryHandler.Handle(searchString);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup([Required] CreateGroupRequest request)
        {
            var id = await _createGroup.Execute(request.Name);
            return Created("api/groups", id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup([FromRoute, ValidGuid] string id)
        {
            await _deleteGroup.Execute(id);
            return Ok();
        }
    }
}