using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.Interfaces;
using TBS.Application.Services;
using TBS.Domain.Entitites;
using TBS.Domain.Interfaces;
using TBS.Infrastructure.Data;
using TBS.Infrastructure.Repositories;
using TBS.Infrastructure.Services;
using TBS.Infrastructure.Settings;


namespace TBS.Infrastructure
{
    public static class DependencyInjection
    {
        public static  IServiceCollection AddInfrastructure(this IServiceCollection services
            ,IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = true;
            })
         .AddRoles<IdentityRole<Guid>>() 
         .AddEntityFrameworkStores<AppDbContext>()
         .AddDefaultTokenProviders();


           
            var jwtSettings = configuration.GetSection("Jwt");
            var secretKey = jwtSettings["SecretKey"];

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidateAudience = true,
                    ValidAudience = jwtSettings["Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IFlightRepository, FlightRepository>();
            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IHotelBookingRepository, HotelBookingRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IFlightBookingRepository, FlightBookingRepository>();
            services.AddScoped<IAirlineRepository, AirlineRepository>();
            services.AddScoped<IAirportRepository, AirportRepository>();
            services.AddScoped<IPassengerRepository, PassengerRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IEmailService, EmailService>();

            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());

            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));


            return services;
        }
    }
}
