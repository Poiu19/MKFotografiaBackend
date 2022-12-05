using SixLabors.ImageSharp;
using Shorthand.ImageSharp.WebP;
namespace MKFotografiaBackend.Helpers
{
    public class ImageConverter
    {
        public static void ConvertFormFileImageToWebpAndSave(IFormFile image, string savePath)
        {
            using (var memoryStream = new MemoryStream())
            {
                image.CopyTo(memoryStream);
                Image.Load(memoryStream).Save(savePath, new WebPEncoder());
            }
        }
    }
}
