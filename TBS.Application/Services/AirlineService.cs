using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var airline = _mapper.Map<Airline>(dto);
            await _unitOfWork.AirlineRepository.AddAsync(airline);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<AirlineDto>(airline);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<AirlineDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<AirlineDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<AirlineDto> UpdateAsync(Guid id, UpdateAirlineDto dto, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
