using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.DTOs.Auth;

namespace TBS.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto,CancellationToken cancellationToken=default);
        Task<AuthResponseDto> LoginAsync(LoginDto dto,CancellationToken cancellationToken=default);
        Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto dto,CancellationToken cancellationToken=default);
        Task<string> ForgotPasswordAsync(ForgotPasswordDto dto,CancellationToken cancellationToken=default);
        Task<bool> ResetPasswordAsync(ResetPasswordDto dto,CancellationToken cancellationToken=default);
        Task<bool> RevokeTokenAsync(RevokeTokenDto dto, CancellationToken cancellationToken = default);

    }
}
