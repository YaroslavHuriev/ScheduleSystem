namespace ScheduleSystem.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ICreateUserUseCase _createUser;

        public UsersController(ICreateUserUseCase createUser)
        {
            _createUser = createUser;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserRequest request)
        {
            await _createUser.Execute(new CreateUserUseCaseInput
            {
                Password = request.Password,
                Username = request.Username
            });
            return Created("api/users", null);
        }
    }
}