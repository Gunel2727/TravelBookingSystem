using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TBS.Application.Common;
using TBS.Application.DTOs.Auth;
using TBS.Application.Interfaces;

namespace TBS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterDto dto,CancellationToken cancellationToken=default)
        {
            var register = await _authService.RegisterAsync(dto,cancellationToken);
            return Ok(ResponseModel<AuthResponseDto>.Ok(register));
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto dto, CancellationToken cancellationToken = default)
        {
            var login = await _authService.LoginAsync(dto, cancellationToken);
            return Ok(ResponseModel<AuthResponseDto>.Ok(login));
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogoutAsync([FromBody] RevokeTokenDto dto, CancellationToken cancellationToken = default)
        {
            await _authService.RevokeTokenAsync(dto, cancellationToken);
            return Ok(ResponseModel<bool>.Ok(true));
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordDto dto, CancellationToken cancellationToken = default)
        {
            var resetToken = await _authService.ForgotPasswordAsync(dto, cancellationToken);
            return Ok(ResponseModel<string>.Ok(resetToken));
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordDto dto, CancellationToken cancellationToken = default)
        {
            var result = await _authService.ResetPasswordAsync(dto, cancellationToken);
            return Ok(ResponseModel<bool>.Ok(result));
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenDto dto, CancellationToken cancellationToken = default)
        {
            var result = await _authService.RefreshTokenAsync(dto, cancellationToken);
            return Ok(ResponseModel<AuthResponseDto>.Ok(result));
        }

    }
}
