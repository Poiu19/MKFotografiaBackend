using MKFotografiaBackend.Helpers;
using MKFotografiaBackend.Models;

namespace MKFotografiaBackend.Services
{
    public enum PhotoType
    {
        SLIDER_PHOTO
    }
    public interface IFileService
    {
        byte[] GetPhoto(PhotoType type, string relativePath);
        string SavePhoto(PhotoType type, IFormFile photo);
    }
    public class FileService : IFileService
    {
        private readonly ApplicationData _settings;
        public FileService(ApplicationData settings)
        {
            _settings = settings;
            if (!Directory.Exists(_settings.GetGalleryFolderPath()))
            {
                Directory.CreateDirectory(_settings.GetGalleryFolderPath());
            }
            if (!Directory.Exists(_settings.GetPostFolderPath()))
            {
                Directory.CreateDirectory(_settings.GetPostFolderPath());
            }
            if (!Directory.Exists(_settings.GetSliderPhotoFolderPath()))
            {
                Directory.CreateDirectory(_settings.GetSliderPhotoFolderPath());
            }
        }

        public byte[] GetPhoto(PhotoType type, string relativePath)
        {
            string directoryPath = "";
            switch (type)
            {
                case PhotoType.SLIDER_PHOTO:
                    directoryPath = _settings.GetSliderPhotoFolderPath();
                    break;
                default:
                    throw new NotImplementedException($"Nieznana wartość {type.ToString()}");
            }
            var absolutePath = Path.Combine(directoryPath, relativePath);
            byte[] bytes = System.IO.File.ReadAllBytes(absolutePath);
            return bytes;
        }

        public string SavePhoto(PhotoType type, IFormFile photo)
        {
            var hash = Algorithms.HashString($"{photo.FileName}{DateTime.Now.ToString()}");
            string path = "";
            switch (type)
            {
                case PhotoType.SLIDER_PHOTO:
                    path = Path.Combine(_settings.GetSliderPhotoFolderPath(), hash);
                    break;
                default:
                    throw new NotImplementedException($"Nieznana wartość {type.ToString()}");
            }
            ImageConverter.ConvertFormFileImageToWebpAndSaveAsync(photo, path);
            return $"{hash}.webp";
        }
    }
}
