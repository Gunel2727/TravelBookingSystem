using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.DTOs.Auth;
using TBS.Application.Interfaces;
using TBS.Domain.Entitites;
using Microsoft.Extensions.Configuration;
using TBS.Application.Common;

namespace TBS.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        
        public AuthService(UserManager<ApplicationUser> userManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if(user==null)
            {
                throw new UnauthorizedException("Email ve ya sifre yanlisdir");
            }
            bool isPasswordTrue = await _userManager.CheckPasswordAsync(user, dto.Password);
            if(!isPasswordTrue)
            {
                throw new UnauthorizedException("Email və ya şifrə yanlışdır");
            }

        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {

            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if(existingUser!=null)
            {
                throw new BadRequestException("Bu email ünvanı ilə artıq qeydiyyatdan keçilib.");
            }
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(",", result.Errors.Select(e => e.Description));
                throw new BadRequestException($"Qeydiyyat uğursuz oldu: {errors}");
            }

            return new AuthResponseDto
            {
                IsSuccess = true,
                Message = "İstifadəçi uğurla qeydiyyatdan keçdi."
            };

        }
    }
}
