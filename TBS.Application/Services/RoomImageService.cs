using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.DTOs.Room;
using TBS.Application.Interfaces;
using TBS.Domain.Entitites;
using TBS.Domain.Interfaces;

namespace TBS.Application.Services
{
    public class RoomImageService : IRoomImageService
    {
        private readonly IUnitOfWork  _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RoomImageService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<RoomImageDto> UploadImageAsync(UploadRoomImageDto uploadRoomImageDto)
        {
            if(uploadRoomImageDto.Image == null || uploadRoomImageDto.Image.Length == 0)
            {
                throw new ArgumentException("No image file provided.");
            }

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath,"images","rooms");
            if(!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName= Guid.NewGuid().ToString() + Path.GetExtension(uploadRoomImageDto.Image.FileName);
            var filePath=Path.Combine(uploadsFolder, uniqueFileName);

            using(var fileStream=new FileStream(filePath,FileMode.Create))
            {
                await uploadRoomImageDto.Image.CopyToAsync(fileStream);
            }

            var relativePath = $"/images/rooms/{uniqueFileName}";
            var roomImage=_mapper.Map<RoomImage>(uploadRoomImageDto);
            roomImage.ImageUrl = relativePath;

            await _unitOfWork.RoomImageRepository.AddAsync(roomImage);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<RoomImageDto>(roomImage);


        }
    }
}
