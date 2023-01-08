using AutoMapper;
using MKFotografiaBackend.Entities;
using MKFotografiaBackend.Exceptions;
using MKFotografiaBackend.Models.Incoming;
using MKFotografiaBackend.Models.Outgoing;

namespace MKFotografiaBackend.Services
{
    public interface ISliderPhotoService
    {
        List<SliderPhotoDto> GetAll();
        byte[] GetPhoto(int photoId);
        int UploadPhoto(UploadSliderPhotoDto form);
    }
    public class SliderPhotoService : ISliderPhotoService
    {
        private readonly MKDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public SliderPhotoService(MKDbContext dbContext, IMapper mapper, IFileService fileService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _fileService = fileService;
        }
        public List<SliderPhotoDto> GetAll()
        {
            var sliderPhotoEntities = _dbContext
                .SliderPhotos
                .OrderBy(sliderPhoto => sliderPhoto.Order)
                .ToList();
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
            return _fileService.GetPhoto(PhotoType.SLIDER_PHOTO, photo.Path);
        }

        public int UploadPhoto(UploadSliderPhotoDto form)
        {
            var relativePath = _fileService.SavePhoto(PhotoType.SLIDER_PHOTO, form.Photo);
            var photoEntity = _mapper.Map<SliderPhoto>(form);
            photoEntity.Path = relativePath;
            _dbContext.SliderPhotos.Add(photoEntity);
            _dbContext.SaveChanges();
            return photoEntity.Id;
        }
    }
}
