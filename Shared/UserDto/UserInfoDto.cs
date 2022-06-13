namespace QuintrixFullstack.Shared.Dto;

public class UserInfoDto
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public DateTime Created { get; set; }
    public string? Roles { get; set; }
}