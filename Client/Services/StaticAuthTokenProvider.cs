namespace QuintrixFullstack.Client.Services;

public class StaticAuthTokenProvider : IAuthTokenProvider
{
    public string? Token { get; set; }
    public StaticAuthTokenProvider()
    {
        Token = "eyJhbGciOiJSUzUxMiIsInR5cCI6IkpXVCIsImN0eSI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiSm9uYXRoYW4iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJHaWdhY2hhZCIsImV4cCI6MTc1NTEwNTUwMiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzE3NyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjcxNzcifQ.RH_A0iFLhdJpJSqRU7x1z0rMmL7ge6Vpufk1cyXJMpYgGx80LfIPGe9ZJwG97zNLhqV64okun3qaXqoFCwXUtqeV3FF_t60hmRU_OiZLN0QbbW3ifjV-Q9nKTT9Z2blhTdWG3q-yBJib3EJ-kSPWHfyPUeYbfq_qhvX2ZzF1EPB0jc3ITgTznpEuuRZ-gJKf3W8TYm-g9OGLSd7S_JSwGuuy48tRVnzTqHXNJ7Pwmf8mEERAJzq_OhueC2YzPj0pzdXDATDKHG8heXucATuSQaZy4Or_Q_DwMxjXHJIdkXRCp2b90BpEFY8rUjfUiRnUwSE-Sus-YnRYB8-IIqd-kg";
    }
}