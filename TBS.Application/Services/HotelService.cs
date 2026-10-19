using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.Common;
using TBS.Application.DTOs.Hotel;
using TBS.Application.Interfaces;
using TBS.Domain.Common;
using TBS.Domain.Entitites;
using TBS.Domain.Interfaces;

namespace TBS.Application.Services
{
    public class HotelService : IHotelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public HotelService(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<HotelDto> AddAsync(CreateHotelDto dto, CancellationToken cancellationToken = default)
        {
            var hotel = _mapper.Map<Hotel>(dto);
            await _unitOfWork.HotelRepository.AddAsync(hotel);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveAsync("hotels_all", cancellationToken);
            var createdHotel = await _unitOfWork.HotelRepository.GetByIdAsync(hotel.Id);
            return _mapper.Map<HotelDto>(createdHotel?? hotel);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var hotel = await _unitOfWork.HotelRepository.GetByIdAsync(id,cancellationToken);
            if (hotel == null) throw new NotFoundException(ErrorMessages.HotelNotFound);
             _unitOfWork.HotelRepository.Delete(hotel);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveAsync($"hotel_{id}", cancellationToken);
            await _cacheService.RemoveAsync("hotels_all", cancellationToken);
        }

        public async Task<IList<HotelDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var chacheKey= "hotels_all";
            var cachedHotels = await _cacheService.GetAsync<IList<HotelDto>>(chacheKey, cancellationToken);
            if(cachedHotels != null)
            {
                return cachedHotels;
            }
            
            var hotels = await _unitOfWork.HotelRepository.GetAllAsync(cancellationToken);
            var hotelDtos= _mapper.Map<IList<HotelDto>>(hotels);
            await _cacheService.SetAsync(chacheKey, hotelDtos, TimeSpan.FromMinutes(30), cancellationToken);
            return hotelDtos;
        }

        public async Task<HotelDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            string cacheKey = $"hotel_{id}";
            var chachedHotels=await _cacheService.GetAsync<HotelDto>(cacheKey, cancellationToken);
            if(chachedHotels != null)
            {
                return chachedHotels;
            }

            var hotel = await _unitOfWork.HotelRepository.GetByIdAsync(id,cancellationToken);
            if (hotel == null) throw new NotFoundException(ErrorMessages.HotelNotFound);
            var hotelDto= _mapper.Map<HotelDto>(hotel);
            await _cacheService.SetAsync(cacheKey, hotelDto, TimeSpan.FromMinutes(30), cancellationToken);
            return hotelDto;
        }

        public async Task<PagedList<HotelDto>> GetFilteredHotelsAsync(HotelFilterDto dto, CancellationToken cancellationToken = default)
        {
            string cacheKey = $"hotels_filter_{dto.SearchTerm}_{dto.CityId}_{dto.MinPrice}_{dto.MaxPrice}_{dto.Rating}_{dto.PageNumber}_{dto.PageSize}";
            var cachedHotels = await _cacheService.GetAsync<PagedList<HotelDto>>(cacheKey,cancellationToken);
            if(cachedHotels != null)
            {
                return cachedHotels;
            }
            var hotels = await _unitOfWork.HotelRepository.GetFilteredHotelsAsync(dto.SearchTerm,
                dto.CityId, dto.MinPrice, dto.MaxPrice, dto.Rating,dto.PageNumber, dto.PageSize
                , cancellationToken);
            var hotelDtos = _mapper.Map<IReadOnlyCollection<HotelDto>>(hotels.Items);
            var pagedResult= new PagedList<HotelDto>(
                 hotelDtos,
                 hotels.TotalCount,
                 hotels.PageNumber,
                 dto.PageSize);
            await _cacheService.SetAsync(cacheKey,pagedResult,TimeSpan.FromMinutes(10),cancellationToken);
            return pagedResult;
        }

        public async Task<HotelDto> UpdateAsync(Guid id, UpdateHotelDto dto, CancellationToken cancellationToken = default)
        {
            var hotel = await _unitOfWork.HotelRepository.GetByIdAsync(id,cancellationToken);
            if (hotel == null) throw new NotFoundException(ErrorMessages.HotelNotFound);

            _mapper.Map(dto, hotel);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveAsync($"hotel_{id}", cancellationToken);
            await _cacheService.RemoveAsync("hotels_all", cancellationToken);
            var updatedHotel = await _unitOfWork.HotelRepository.GetByIdAsync(id,cancellationToken);
            return _mapper.Map<HotelDto>(updatedHotel ?? hotel);
        }
        
    }
}
