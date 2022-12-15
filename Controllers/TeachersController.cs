namespace ScheduleSystem.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly IGetTeachersListQueryHandler _getTeachersListQueryHandler;
        private readonly ICreateTeacherUseCase _createTeacherUseCase;
        private readonly IDeleteTeacherUseCase _deleteTeacherUseCase;

        public TeachersController(IGetTeachersListQueryHandler getTeachersListQueryHandler, ICreateTeacherUseCase createTeacherUseCase, IDeleteTeacherUseCase deleteTeacherUseCase)
        {
            _getTeachersListQueryHandler = getTeachersListQueryHandler;
            _createTeacherUseCase = createTeacherUseCase;
            _deleteTeacherUseCase = deleteTeacherUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetTeachersList([FromQuery] string? searchString = null)
        {
            var result = await _getTeachersListQueryHandler.Handle(searchString);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostTeacher([Required] CreateTeacherRequest request)
        {
            var id = await _createTeacherUseCase.Execute(new CreateTeacherUseCaseInput
            {
                FirstName = request.FirstName,
                Surname = request.Surname
            });
            return Created("api/teachers", id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute, Required] string id)
        {
            await _deleteTeacherUseCase.Execute(id);
            return Ok();
        }
    }
}