using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.DTOs.Airport;
using TBS.Application.DTOs.City;
using TBS.Application.DTOs.Country;
using TBS.Application.DTOs.Flight;
using TBS.Application.DTOs.Hotel;
using TBS.Application.DTOs.HotelBooking;
using TBS.Application.DTOs.Review;
using TBS.Application.DTOs.Room;
using TBS.Domain.Entitites;

namespace TBS.Application.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Country, CountryDto>();
            CreateMap<City, CityDto>()
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name));
            CreateMap<Airport, AirportDto>()
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name));
            CreateMap<Flight, FlightDto>()
             .ForMember(dest => dest.DepartureAirportName, opt => opt.MapFrom(src => src.DepartureAirport.Name))
             .ForMember(dest => dest.ArrivalAirportName, opt => opt.MapFrom(src => src.ArrivalAirport.Name))
             .ForMember(dest => dest.AirlineName, opt => opt.MapFrom(src => src.Airline.Name));

            CreateMap<CreateFlightDto, Flight>();
            CreateMap<UpdateFlightDto, Flight>();

            CreateMap<Room, RoomDto>()
                .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Hotel.Name));
               
            CreateMap<CreateRoomDto, Room>();
            CreateMap<UpdateRoomDto, Room>();

            CreateMap<RoomImage, RoomImageDto>();

            CreateMap<Review, ReviewDto>();

            CreateMap<HotelAmenity,HotelAmenityDto>();

            CreateMap<Hotel, HotelDto>()
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name));
            CreateMap<CreateHotelDto, Hotel>();
            CreateMap<UpdateHotelDto, Hotel>();

            CreateMap<HotelImage, HotelImageDto>();

            CreateMap<HotelBooking, HotelBookingDto>()
                .ForMember(dest => dest.ApplicationUserName, opt => opt.MapFrom(src => src.ApplicationUser.UserName))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<CreateHotelBookingDto, HotelBooking>();
            CreateMap<UpdateHotelBookingDto, HotelBooking>();

            CreateMap<HotelBookingRoom, HotelBookingRoomDto>()
                .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room.Name));

        }
     }
}
