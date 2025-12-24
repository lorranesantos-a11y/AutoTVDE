using AutoTvde.Api.Auth;
using AutoTvde.Api.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly JwtTokenService _jwtTokenService;

    public AuthController(JwtTokenService jwtTokenService)
    {
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("login")]
    public ActionResult<LoginResponseDto> Login(LoginRequestDto dto)
    {
        var user = FakeUserStore.Users.FirstOrDefault(u =>
            u.Email.Equals(dto.Email, StringComparison.OrdinalIgnoreCase) &&
            u.Password == dto.Password
        );

        if (user is null)
            return Unauthorized("Invalid credentials.");

        var token = _jwtTokenService.GenerateToken(user.Email, user.Role);

        return Ok(new LoginResponseDto
        {
            Token = token,
            Role = user.Role,
            ExpiresAtUtc = DateTime.UtcNow.AddHours(2)
        });
    }
}
