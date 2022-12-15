public interface IUserRepository
{
    Task<UserDTO> GetUser(string email);
    Task CreateUser(string username, string password);
}