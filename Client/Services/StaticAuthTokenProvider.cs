namespace QuintrixFullstack.Client.Services;

public class StaticAuthTokenProvider : IAuthTokenProvider
{
    public string? Token { get; set; }
    public StaticAuthTokenProvider()
    {
    }
}