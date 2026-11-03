using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TBS.Application.Common;
using TBS.Application.DTOs.Auth;
using TBS.Application.Interfaces;
using TBS.Domain.Entitites;

namespace TBS.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IAppDbContext _context;

        public AuthService(UserManager<ApplicationUser> userManager,IConfiguration configuration,IMapper mapper,IAppDbContext context)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
            _context = context;
        }

        public async Task<string> ForgotPasswordAsync(ForgotPasswordDto dto, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if(user==null)
            {
                throw new NotFoundException("İstifadəçi tapılmadı");
            }
            var resetToken=await _userManager.GeneratePasswordResetTokenAsync(user);
            return resetToken;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto,CancellationToken cancellationToken = default)
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
            

            var(accessToken, expiration) = await GenerateAccessTokenAsync(user);


            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                ApplicationUserId = user.Id
            };

            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new AuthResponseDto
            {
                IsSuccess = true,
                Token = accessToken,
                Expiration = expiration,
                RefreshToken=refreshToken.Token,
                Message = "Uğurlu giriş"
            };

        }

        public async Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto dto, CancellationToken cancellationToken = default)
        {
            var refreshToken = await _context.RefreshTokens.Include(rt => rt.ApplicationUser)
                .FirstOrDefaultAsync(rt => rt.Token == dto.RefreshToken, cancellationToken);
            if(refreshToken == null)
            {
                throw new UnauthorizedException("Refresh token tapılmadı.");
            }
            if(refreshToken.RevokedAt != null)
            {
                throw new UnauthorizedException("Refresh token artıq ləğv edilmişdir.");
            }
            if ( refreshToken.ExpiresAt < DateTime.UtcNow)
            {
                throw new UnauthorizedException("Refresh token vaxtı bitmişdir.");
            }
            refreshToken.RevokedAt = DateTime.UtcNow;
            var(newAccessToken,expiration)=await GenerateAccessTokenAsync(refreshToken.ApplicationUser);
            
            var newRefreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                ApplicationUserId = refreshToken.ApplicationUserId
            };
            await _context.RefreshTokens.AddAsync(newRefreshToken, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new AuthResponseDto
            {
                IsSuccess = true,
                Token = newAccessToken,
                Expiration = expiration,
                RefreshToken = newRefreshToken.Token,
                Message = "Token uğurla yeniləndi."
            };

        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto, CancellationToken cancellationToken = default)
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

        public async Task<bool> ResetPasswordAsync(ResetPasswordDto dto, CancellationToken cancellationToken = default)
        {
            var user=await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                throw new NotFoundException("İstifadəçi tapılmadı");
            }
            var result =await _userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new BadRequestException($"Şifrə sıfırlama uğursuz oldu: {errors}");
            }

            return true;
        }

        public async Task<bool> RevokeTokenAsync(RevokeTokenDto dto, CancellationToken cancellationToken = default)
        {
           var refreshToken=await _context.RefreshTokens.FirstOrDefaultAsync(rt=>rt.Token == dto.RefreshToken, cancellationToken);
           if(refreshToken==null)
                throw new NotFoundException("Refresh token tapılmadı.");
           refreshToken.RevokedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        private async Task<(string TokenString, DateTime Expiration)> GenerateAccessTokenAsync(ApplicationUser user)
        {
            var roles= await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.GivenName, user.FirstName ?? string.Empty),
                new Claim(ClaimTypes.Surname, user.LastName ?? string.Empty)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            string secretKey = _configuration["Jwt:SecretKey"];
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new InvalidOperationException("JWT SecretKey appsettings.json faylında tapılmadı!");
            }

            var keyInBytes = Encoding.UTF8.GetBytes(secretKey);
            var securityKey = new SymmetricSecurityKey(keyInBytes);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddHours(2);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            string tokenString = tokenHandler.WriteToken(token);

            return (tokenString, expires);
        }
    }
}
