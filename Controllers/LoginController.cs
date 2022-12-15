namespace ScheduleSystem.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginUseCase _loginUseCase;
        public LoginController(ILoginUseCase loginUseCase)
        {
            _loginUseCase = loginUseCase;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            IActionResult response = Unauthorized();
            var token = await _loginUseCase.Execute(new LoginUseCaseInput{
                Password = request.Password,
                Username = request.UserName
            });
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}