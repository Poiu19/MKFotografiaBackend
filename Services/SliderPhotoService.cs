using AutoMapper;
using MKFotografiaBackend.Entities;
using MKFotografiaBackend.Exceptions;
using MKFotografiaBackend.Models.Outgoing;

namespace MKFotografiaBackend.Services
{
    public interface ISliderPhotoService
    {
        List<SliderPhotoDto> GetAll();
        byte[] GetPhoto(int photoId);
    }
    public class SliderPhotoService : ISliderPhotoService
    {
        private readonly MKDbContext _dbContext;
        private readonly IMapper _mapper;
        public SliderPhotoService(MKDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<SliderPhotoDto> GetAll()
        {
            var sliderPhotoEntities = _dbContext.SliderPhotos.ToList();
            return _mapper.Map<List<SliderPhotoDto>>(sliderPhotoEntities);
        }

        public byte[] GetPhoto(int photoId)
        {
            var photo = _dbContext
                .SliderPhotos
                .FirstOrDefault(photo => photo.Id == photoId);
            if (photo is null)
            {
                throw new BadRequestException($"Brak zdjęcia z id={photoId}");
            }

            var dirPath = "/var/mkbackend";
            string fullPath = Path.Combine(dirPath, photo.Path);
            byte[] bytes = System.IO.File.ReadAllBytes(fullPath);
            return bytes;
        }
    }
}
