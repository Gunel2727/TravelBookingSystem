using AutoMapper;
using Microsoft.AspNetCore.Hosting;
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

        public async Task DeleteAsync(Guid imageId, CancellationToken cancellationToken = default)
        {
            var roomImage=await _unitOfWork.RoomImageRepository.GetByIdAsync(imageId, cancellationToken);
            if (roomImage == null)
            {
                throw new ArgumentException(ErrorMessages.RoomImageNotFound);
            }
            if(!string.IsNullOrEmpty(roomImage.ImageUrl))
            {
                var filePath=Path.Combine(_webHostEnvironment.WebRootPath,roomImage.ImageUrl.TrimStart('/','\\'));
                if(File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            _unitOfWork.RoomImageRepository.Delete(roomImage);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<RoomImageDto>> GetImagesByRoomIdAsync(Guid roomId, CancellationToken cancellationToken = default)
        {
            var images=await _unitOfWork.RoomImageRepository.GetImagesByRoomIdAsync(roomId, cancellationToken);
            return _mapper.Map<IList<RoomImageDto>>(images);
        }

        public async Task<RoomImageDto> UploadImageAsync(UploadRoomImageDto uploadRoomImageDto,CancellationToken cancellationToken = default)
        {
            var room = await _unitOfWork.RoomRepository.GetByIdAsync(uploadRoomImageDto.RoomId, cancellationToken);
            if(room == null)
            {
                throw new ArgumentException(ErrorMessages.RoomNotFound);
            }
            if (uploadRoomImageDto.Image == null || uploadRoomImageDto.Image.Length == 0)
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
                await uploadRoomImageDto.Image.CopyToAsync(fileStream,cancellationToken);
            }

            var relativePath = $"/images/rooms/{uniqueFileName}";
            var roomImage=_mapper.Map<RoomImage>(uploadRoomImageDto);
            roomImage.ImageUrl = relativePath;
            roomImage.Room = room;

            await _unitOfWork.RoomImageRepository.AddAsync(roomImage);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<RoomImageDto>(roomImage);


        }
    }
}
