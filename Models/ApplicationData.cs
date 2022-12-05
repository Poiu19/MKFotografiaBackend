namespace MKFotografiaBackend.Models
{
    public class ApplicationData
    {
        public string AppDataAbsolutePath { get; set; }
        public string UploadsFolder { get; set; }
        public string PostFolder { get; set; }
        public string GalleryFolder { get; set; }
        public bool UseHttps { get; set; }
        public string GetPostFolderPath()
        {
            return Path.Combine(AppDataAbsolutePath, UploadsFolder, PostFolder);
        }
        public string GetGalleryFolderPath()
        {
            return Path.Combine(AppDataAbsolutePath, UploadsFolder, GalleryFolder);
        }
    }
}
