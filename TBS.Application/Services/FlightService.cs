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
        public async Task<FlightDto> AddAsync(CreateFlightDto dto,CancellationToken cancellationToken=default)
        {
            var flight = _mapper.Map<Flight>(dto);
            flight.Status = FlightStatus.Scheduled;
            await _unitOfWork.FlightRepository.AddAsync(flight);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var createdFlight = await _unitOfWork.FlightRepository.GetByIdAsync(flight.Id,cancellationToken);
            return _mapper.Map<FlightDto>(createdFlight ?? flight);
        }

        public async Task DeleteAsync(Guid id,CancellationToken cancellationToken=default)
        {
            var flight = await _unitOfWork.FlightRepository.GetByIdAsync(id,cancellationToken);
            if (flight == null) throw new NotFoundException(ErrorMessages.FlightNotFound);
            _unitOfWork.FlightRepository.Delete(flight);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<FlightDto>> GetAllAsync(CancellationToken cancellationToken=default)
        {
            var flights = await _unitOfWork.FlightRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IList<FlightDto>>(flights);
        }

        public async Task<FlightDto> GetByIdAsync(Guid id,CancellationToken cancellationToken)
        {
            var flight = await _unitOfWork.FlightRepository.GetByIdAsync(id,cancellationToken);
            if (flight == null) throw new NotFoundException(ErrorMessages.FlightNotFound);
            return _mapper.Map<FlightDto>(flight);
        
        }

        public async Task<IList<FlightDto>> GetFilteredAsync(FilterFlightDto dto,CancellationToken cancellationToken=default)
        {
            var flights = await _unitOfWork.FlightRepository.GetFilteredAsync(dto.DepartureAirportId, dto.ArrivalAirportId, dto.DepartureDate,cancellationToken);
            return _mapper.Map<IList<FlightDto>>(flights);
        }

        public async Task<FlightDto> UpdateAsync(Guid id, UpdateFlightDto dto,CancellationToken cancellationToken=default)
        {
            var flight = await _unitOfWork.FlightRepository.GetByIdAsync(id,cancellationToken);
            if (flight == null) throw new NotFoundException(ErrorMessages.FlightNotFound);

            _mapper.Map(dto, flight);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var updatedFlight = await _unitOfWork.FlightRepository.GetByIdAsync(id,cancellationToken);
            return _mapper.Map<FlightDto>(updatedFlight ?? flight);
        }
    }
}
