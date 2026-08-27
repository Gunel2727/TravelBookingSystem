using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.DTOs.Room;
using TBS.Application.Interfaces;
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

        public Task<RoomDto> AddAsync(CreateRoomDto dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<RoomDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RoomDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<RoomDto>> GetFilteredAsync(FilterRoomDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<RoomDto> UpdateAsync(UpdateRoomDto dto, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
