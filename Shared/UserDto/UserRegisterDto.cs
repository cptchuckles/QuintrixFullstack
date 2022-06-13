using System.ComponentModel.DataAnnotations;

namespace QuintrixFullstack.Shared.Dto;

public class UserRegisterDto
{
    [Required]
    [MinLength(5)]
    public string Username { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(10)]
    public string Password { get; set; }
}