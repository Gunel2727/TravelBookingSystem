using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.Common;
using TBS.Application.DTOs.Room;
using TBS.Application.Interfaces;
using TBS.Domain.Entitites;
using TBS.Domain.Interfaces;

namespace TBS.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork    _unitOfWork;
        private readonly IMapper _mapper;
        public RoomService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RoomDto> AddAsync(CreateRoomDto dto)
        {
            var room = _mapper.Map<Room>(dto);
            await _unitOfWork.RoomRepository.AddAsync(room);
            await _unitOfWork.SaveChangesAsync();
            var createdRoom=await _unitOfWork.RoomRepository.GetByIdAsync(room.Id);
            return _mapper.Map<RoomDto>(createdRoom?? room);
        }

        public async Task DeleteAsync(Guid id)
        {
            var room = await _unitOfWork.RoomRepository.GetByIdAsync(id);
            if(room==null)
            {
                throw new NotFoundException(ErrorMessages.RoomNotFound);
            }
            _unitOfWork.RoomRepository.Delete(room);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IList<RoomDto>> GetAllAsync()
        {
            var rooms=await _unitOfWork.RoomRepository.GetAllAsync();
            return _mapper.Map<IList<RoomDto>>(rooms);
        }

        public async Task<RoomDto> GetByIdAsync(Guid id)
        {
            var room = await _unitOfWork.RoomRepository.GetByIdAsync(id);
            if (room == null) throw new NotFoundException(ErrorMessages.RoomNotFound);
            return _mapper.Map<RoomDto>(room);
        }

        public async Task<IList<RoomDto>> GetFilteredAsync(FilterRoomDto dto)
        {
            var rooms = await _unitOfWork.RoomRepository.GetFilteredAsync(dto.HotelId,dto.MinPriceForNight,dto.MaxPriceForNight,dto.MinCapacity);
            return _mapper.Map<IList<RoomDto>>(rooms);
        }

        public async Task<RoomDto> UpdateAsync(Guid id,UpdateRoomDto dto)
        {
            var room = await _unitOfWork.RoomRepository.GetByIdAsync(id);
            if(room==null) throw new NotFoundException(ErrorMessages.RoomNotFound);
            _mapper.Map(dto,room);
           
            await _unitOfWork.SaveChangesAsync();
            var updatedRoom = await _unitOfWork.RoomRepository.GetByIdAsync(id);
            return _mapper.Map<RoomDto>(updatedRoom??room);
        }
    }
}
