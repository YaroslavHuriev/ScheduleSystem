
public class LoginUseCase : ILoginUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _config;
    public LoginUseCase(IUserRepository userRepository, ITokenService tokenService, IConfiguration config)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _config = config;
    }

    public async Task<string?> Execute(LoginUseCaseInput input)
    {
        var validUser = await _userRepository.GetUser(input.Username);

        if (validUser != null && validUser.Password == input.Password)
        {
            var generatedToken = _tokenService.BuildToken(
                _config["Jwt:Key"].ToString(),
                _config["Jwt:Issuer"].ToString(),
                _config["Jwt:Audience"].ToString(),
                validUser);
            
            return generatedToken;
        }
        return null;
    }
}