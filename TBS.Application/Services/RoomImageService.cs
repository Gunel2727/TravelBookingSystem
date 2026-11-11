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
    public class RoomImageService : IRoomImageService
    {
        private readonly IUnitOfWork  _unitOfWork;
        private readonly IMapper _mapper;

        public RoomImageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Task UploadImageAsync(UploadRoomImageDto uploadRoomImageDto)
        {
            throw new NotImplementedException();
        }
    }
}
