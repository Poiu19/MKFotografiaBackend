using Microsoft.AspNetCore.Mvc;
using MKFotografiaBackend.Entities;
using MKFotografiaBackend.Models.Outgoing;
using MKFotografiaBackend.Services;
using System;

namespace MKFotografiaBackend.Controllers
{
    [Route("api/slider-photo")]
    public class SliderPhotoController : ControllerBase
    {
        private readonly ISliderPhotoService _sliderPhotoService;
        public SliderPhotoController(ISliderPhotoService sliderPhotoService) {
            _sliderPhotoService = sliderPhotoService;
        }

        [HttpGet]
        public ActionResult<List<SliderPhotoDto>> GetAll()
        {
            return Ok(_sliderPhotoService.GetAll());
        }
        [HttpGet("{photoId}")]
        public IActionResult GetPhoto([FromRoute] int photoId)
        {
            return File(_sliderPhotoService.GetPhoto(photoId), "image/webp");
        }
    }
}
