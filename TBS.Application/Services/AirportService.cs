using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.Common;
using TBS.Application.DTOs.Airport;
using TBS.Application.Interfaces;
using TBS.Domain.Entitites;
using TBS.Domain.Interfaces;

namespace TBS.Application.Services
{
    public class AirportService : IAirportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AirportService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AirportDto> CreateAsync(CreateAirportDto dto, CancellationToken cancellationToken = default)
        {
            var isUnique=await _unitOfWork.AirportRepository.IsCodeUniqueAsync(dto.Code, cancellationToken);
            if(!isUnique)
            {
                throw new BadRequestException("Bu kodla airport artıq mövcuddur. Zəhmət olmasa, fərqli bir kod seçin.");
            }
            var airport = _mapper.Map<Airport>(dto);
            await _unitOfWork.AirportRepository.AddAsync(airport);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var createdAirport = await _unitOfWork.AirportRepository.GetByIdWithCityAsync(airport.Id, cancellationToken);
            return _mapper.Map<AirportDto>(createdAirport?? airport);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var airport = await _unitOfWork.AirportRepository.GetWithFlightsAsync(id, cancellationToken);
            if (airport == null)
            {
                throw new NotFoundException(ErrorMessages.AirportNotFound);
            }
            if (airport.DepartureFlights.Any() || airport.ArrivalFlights.Any())
            {
                throw new BadRequestException("Bu hava limanı ilə əlaqəli uçuşlar mövcuddur. Silmək mümkün deyil.");

            }
            _unitOfWork.AirportRepository.Delete(airport);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<AirportDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var airports = await _unitOfWork.AirportRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IList<AirportDto>>(airports);
        }

       
        public async Task<AirportDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var airport=await _unitOfWork.AirportRepository.GetByIdWithCityAsync(id, cancellationToken);
            if(airport == null)
            {
                throw new NotFoundException(ErrorMessages.AirportNotFound);
            }
            return _mapper.Map<AirportDto>(airport);
        }

        public async Task<AirportDto> UpdateAsync(Guid id, UpdateAirportDto dto, CancellationToken cancellationToken = default)
        {
            var airport=await _unitOfWork.AirportRepository.GetByIdAsync(id, cancellationToken);
            if(airport == null)
            {
                throw new NotFoundException(ErrorMessages.AirportNotFound);
            }
            if (airport.Code != dto.Code)
            {
                var isUnique = await _unitOfWork.AirportRepository.IsCodeUniqueAsync(dto.Code, cancellationToken);
              if(!isUnique)
                {
                    throw new BadRequestException("Bu kodla airport artıq mövcuddur. Zəhmət olmasa, fərqli bir kod seçin.");
                }
            }
            _mapper.Map(dto, airport);
            
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var updatedAirport = await _unitOfWork.AirportRepository.GetByIdWithCityAsync(id, cancellationToken);
            return _mapper.Map<AirportDto>(updatedAirport ?? airport);
        }
    }
}
