using Microsoft.AspNetCore.Mvc;
using MKFotografiaBackend.Entities;
using MKFotografiaBackend.Models;
using System;

namespace MKFotografiaBackend.Controllers
{
    [Route("api/slider-photo")]
    public class SliderPhotoController : ControllerBase
    {
        private readonly MKDbContext _dbContext;
        public SliderPhotoController(MKDbContext dbContext) {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<List<SliderPhotoDto>> GetAll()
        {
            return Ok(_dbContext.SliderPhotos.ToList());
        }
        [HttpGet("{photoId}")]
        public IActionResult GetPhoto([FromRoute] int photoId)
        {
            var photo = _dbContext
                .SliderPhotos
                .FirstOrDefault(photo => photo.Id == photoId);
            var dirPath = "/var/mkbackend";
            Console.WriteLine(dirPath);
            Console.WriteLine(photo.Path);
            string fullPath = Path.Combine(dirPath, photo.Path);
            Console.WriteLine(fullPath);
            byte[] bytes = System.IO.File.ReadAllBytes(fullPath);
            return File(bytes, "image/webp");
        }
    }
}
