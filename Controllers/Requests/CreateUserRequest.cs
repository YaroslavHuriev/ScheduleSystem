using System.ComponentModel.DataAnnotations;

public class CreateUserRequest
{
    [Required, EmailAddress]
    public string Username { get; set; }
    [Required, MinLength(8)]
    public string Password { get; set; }
}