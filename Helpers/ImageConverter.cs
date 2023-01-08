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
                memoryStream.Position = 0;
                image.CopyTo(memoryStream);
                memoryStream.Position = 0;
                Image.Load(memoryStream).Save($"{savePath}.webp", new WebPEncoder());
            }
        }
    }
}
