public class CreateUserUseCase : ICreateUserUseCase
{
    private readonly IUserRepository _userRepository;

    public CreateUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Execute(CreateUserUseCaseInput input)
    {
        await _userRepository.CreateUser(input.Username, input.Password);
    }
}