using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.Common;
using TBS.Application.DTOs.Flight;
using TBS.Application.Interfaces;
using TBS.Domain.Entitites;
using TBS.Domain.Enums;
using TBS.Domain.Interfaces;

namespace TBS.Application.Services
{
    public class FlightService : IFlightService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public FlightService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<FlightDto> AddAsync(CreateFlightDto dto)
        {
            var flight = _mapper.Map<Flight>(dto);
            flight.Status = FlightStatus.Scheduled;
            await _unitOfWork.FlightRepository.AddAsync(flight);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<FlightDto>(flight);
        }

        public async Task DeleteAsync(Guid id)
        {
            var flight = await _unitOfWork.FlightRepository.GetByIdAsync(id);
            if (flight == null) throw new NotFoundException(ErrorMessages.FlightNotFound);
            _unitOfWork.FlightRepository.Delete(flight);
            await _unitOfWork.SaveChangesAsync();
        }

        public Task<IList<FlightDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<FlightDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<FlightDto>> GetFilteredAsync(Guid? departureAirportId, Guid? arrivalAirportId, DateTime? departureDate)
        {
            throw new NotImplementedException();
        }

        public Task<FlightDto> UpdateAsync(Guid id, UpdateFlightDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
