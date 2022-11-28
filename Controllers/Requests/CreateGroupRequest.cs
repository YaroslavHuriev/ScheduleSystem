using System.ComponentModel.DataAnnotations;

public class CreateGroupRequest
{
    [Required]
    public string Name { get; set; }
}