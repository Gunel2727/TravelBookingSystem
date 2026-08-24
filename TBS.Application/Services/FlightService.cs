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
            var createdFlight = await _unitOfWork.FlightRepository.GetByIdAsync(flight.Id);
            return _mapper.Map<FlightDto>(createdFlight ?? flight);
        }

        public async Task DeleteAsync(Guid id)
        {
            var flight = await _unitOfWork.FlightRepository.GetByIdAsync(id);
            if (flight == null) throw new NotFoundException(ErrorMessages.FlightNotFound);
            _unitOfWork.FlightRepository.Delete(flight);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IList<FlightDto>> GetAllAsync()
        {
            var flights = await _unitOfWork.FlightRepository.GetAllAsync();
            return _mapper.Map<IList<FlightDto>>(flights);
        }

        public async Task<FlightDto> GetByIdAsync(Guid id)
        {
            var flight = await _unitOfWork.FlightRepository.GetByIdAsync(id);
            if (flight == null) throw new NotFoundException(ErrorMessages.FlightNotFound);
            return _mapper.Map<FlightDto>(flight);
        
        }

        public async Task<IList<FlightDto>> GetFilteredAsync(FilterFlightDto dto)
        {
            var flights = await _unitOfWork.FlightRepository.GetFilteredAsync(dto.DepartureAirportId, dto.ArrivalAirportId, dto.DepartureDate);
            return _mapper.Map<IList<FlightDto>>(flights);
        }

        public async Task<FlightDto> UpdateAsync(Guid id, UpdateFlightDto dto)
        {
            var flight = await _unitOfWork.FlightRepository.GetByIdAsync(id);
            if (flight == null) throw new NotFoundException(ErrorMessages.FlightNotFound);

            _mapper.Map(dto, flight);

            _unitOfWork.FlightRepository.Update(flight);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<FlightDto>(flight);
        }
    }
}
