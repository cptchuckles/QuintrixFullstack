namespace QuintrixFullstack.Client.Services;

public interface IAuthTokenProvider
{
    public string? Token { get; set; }
}