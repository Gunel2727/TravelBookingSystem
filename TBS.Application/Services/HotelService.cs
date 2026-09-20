using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.Common;
using TBS.Application.DTOs.Hotel;
using TBS.Application.Interfaces;
using TBS.Domain.Entitites;
using TBS.Domain.Interfaces;

namespace TBS.Application.Services
{
    public class HotelService : IHotelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HotelService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<HotelDto> AddAsync(CreateHotelDto dto, CancellationToken cancellationToken = default)
        {
            var hotel = _mapper.Map<Hotel>(dto);
            await _unitOfWork.HotelRepository.AddAsync(hotel);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var createdHotel = await _unitOfWork.HotelRepository.GetByIdAsync(hotel.Id);
            return _mapper.Map<HotelDto>(createdHotel?? hotel);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var hotel = await _unitOfWork.HotelRepository.GetByIdAsync(id,cancellationToken);
            if (hotel == null) throw new NotFoundException(ErrorMessages.HotelNotFound);
             _unitOfWork.HotelRepository.Delete(hotel);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<HotelDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var hotels = await _unitOfWork.HotelRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IList<HotelDto>>(hotels);
        }

        public async Task<HotelDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var hotel = await _unitOfWork.HotelRepository.GetByIdAsync(id,cancellationToken);
            if (hotel == null) throw new NotFoundException(ErrorMessages.HotelNotFound);
            return _mapper.Map<HotelDto>(hotel);
        }

        public async Task<IList<HotelDto>> GetFilteredAsync(FilterHotelDto dto, CancellationToken cancellationToken = default)
        {
            var hotels = await _unitOfWork.HotelRepository.GetFilteredAsync(dto.Search, dto.CityId,cancellationToken);
            return _mapper.Map<IList<HotelDto>>(hotels);
        }

        public async Task<HotelDto> UpdateAsync(Guid id, UpdateHotelDto dto, CancellationToken cancellationToken = default)
        {
            var hotel = await _unitOfWork.HotelRepository.GetByIdAsync(id,cancellationToken);
            if (hotel == null) throw new NotFoundException(ErrorMessages.HotelNotFound);

            _mapper.Map(dto, hotel);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var updatedHotel = await _unitOfWork.HotelRepository.GetByIdAsync(id,cancellationToken);
            return _mapper.Map<HotelDto>(updatedHotel ?? hotel);
        }
        
    }
}
