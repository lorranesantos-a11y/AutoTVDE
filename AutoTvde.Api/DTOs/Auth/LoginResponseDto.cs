namespace AutoTvde.Api.DTOs.Auth;

public class LoginResponseDto
{
    public string Token { get; set; } = default!;
    public DateTime ExpiresAtUtc { get; set; }
    public string Role { get; set; } = default!;
}
