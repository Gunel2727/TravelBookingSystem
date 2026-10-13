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
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;

namespace TBS.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        
        public AuthService(UserManager<ApplicationUser> userManager,IConfiguration configuration,IMapper mapper)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
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
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email ?? string.Empty),
                new Claim(ClaimTypes.GivenName, user.FirstName?? string.Empty),
                new Claim(ClaimTypes.Surname,user.LastName??string.Empty)
            };
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role,role));
            }

            string secretKey = _configuration["Jwt:SecretKey"];
            if(string.IsNullOrEmpty(secretKey))
            {
                throw new InvalidOperationException("JWT SecretKey appsettings.json faylında tapılmadı!");
            }
            var keyInBytes = Encoding.UTF8.GetBytes(secretKey);
            var securityKey = new SymmetricSecurityKey(keyInBytes);

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer:_configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims:claims,
                expires:DateTime.UtcNow.AddHours(2),
                signingCredentials:credentials
                );

            var tokenHandler = new JwtSecurityTokenHandler();
            string tokenString = tokenHandler.WriteToken(token);

            return new AuthResponseDto
            {
                IsSuccess = true,
                Token = tokenString,
                Expiration = token.ValidTo,
                RefreshToken=Guid.NewGuid().ToString(),
                Message = "Uğurlu giriş"
            };

        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {

            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if(existingUser!=null)
            {
                throw new BadRequestException("Bu email ünvanı ilə artıq qeydiyyatdan keçilib.");
            }
            var user = _mapper.Map<ApplicationUser>(dto);
            
            var result = await _userManager.CreateAsync(user, dto.Password);
       

            if (!result.Succeeded)
            {
                var errors = string.Join(",", result.Errors.Select(e => e.Description));
                throw new BadRequestException($"Qeydiyyat uğursuz oldu: {errors}");
            }
            await _userManager.AddToRoleAsync(user, "User");

            return new AuthResponseDto
            {
                IsSuccess = true,
                Message = "İstifadəçi uğurla qeydiyyatdan keçdi."
            };

        }
    }
}
