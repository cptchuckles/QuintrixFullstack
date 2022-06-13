using System.ComponentModel.DataAnnotations;

namespace QuintrixFullstack.Shared.Dto;

public class UserLoginDto
{
    [Required]
    public string UsernameOrEmail { get; set; }
    [Required]
    public string Password { get; set; }
}