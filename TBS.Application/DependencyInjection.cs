using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.Interfaces;
using TBS.Application.Profiles;
using TBS.Application.Services;
using TBS.Application.Validators;

namespace TBS.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

            services.AddScoped<IFlightService, FlightService>();
            services.AddScoped<IHotelService, HotelService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IHotelBookingService, HotelBookingService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IFlightBookingService, FlightBookingService>();
            services.AddScoped<IAirlineService, AirlineService>();
            services.AddScoped<IAirportService, AirportService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IRoomImageService, RoomImageService>();



            services.AddValidatorsFromAssemblyContaining<RegisterValidator>();
           


            return services;
        }
    }
}
