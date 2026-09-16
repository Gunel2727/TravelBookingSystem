using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.Common;
using TBS.Application.DTOs.Airline;
using TBS.Application.Interfaces;
using TBS.Domain.Entitites;
using TBS.Domain.Interfaces;

namespace TBS.Application.Services
{
    public class AirlineService : IAirlineService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AirlineService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<AirlineDto> CreateAsync(CreateAirlineDto dto, CancellationToken cancellationToken = default)
        {
            var isUnique=await _unitOfWork.AirlineRepository.IsCodeUniqueAsync(dto.Code, cancellationToken);
            if ((!isUnique))
            {
                throw new BadRequestException("Bu kodla airline artıq mövcuddur. Zəhmət olmasa, fərqli bir kod seçin.");
            }
            var airline = _mapper.Map<Airline>(dto);
            await _unitOfWork.AirlineRepository.AddAsync(airline);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<AirlineDto>(airline);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var airline=await _unitOfWork.AirlineRepository.GetWithFlightsAsync(id, cancellationToken);

            if (airline == null)
            {
                throw new NotFoundException(ErrorMessages.AirlineNotFound);
            }

            if (airline.Flights.Any())
            {
                throw new BadRequestException("Bu airline silinə bilməz, çünki ona aid uçuşlar mövcuddur.");
            }
          
            
            _unitOfWork.AirlineRepository.Delete(airline);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<AirlineDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var airlines=await _unitOfWork.AirlineRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IList<AirlineDto>>(airlines);
        }

        public async Task<AirlineDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var airline = await _unitOfWork.AirlineRepository.GetByIdAsync(id, cancellationToken);
            if(airline == null)
            {
                throw new NotFoundException(ErrorMessages.AirlineNotFound);
            }
            return _mapper.Map<AirlineDto>(airline);
        }

        public async Task<AirlineDto> UpdateAsync(Guid id, UpdateAirlineDto dto, CancellationToken cancellationToken = default)
        {
            var airline=await _unitOfWork.AirlineRepository.GetByIdAsync(id, cancellationToken);
            if (airline==null)
            {
                throw new NotFoundException(ErrorMessages.AirlineNotFound);
            }
            _mapper.Map(dto, airline);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<AirlineDto>(airline);
        }
    }
}
