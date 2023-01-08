using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MKFotografiaBackend.Entities;
using MKFotografiaBackend.Models.Incoming;
using MKFotografiaBackend.Models.Outgoing;
using MKFotografiaBackend.Services;
using System;

namespace MKFotografiaBackend.Controllers
{
    [Route("api/slider-Photo")]
    public class SliderPhotoController : ControllerBase
    {
        private readonly ISliderPhotoService _sliderPhotoService;
        private readonly ILogger<SliderPhotoController> _logger;
        private readonly IUserContextService _userContextService;
        public SliderPhotoController(ISliderPhotoService sliderPhotoService, ILogger<SliderPhotoController> logger, IUserContextService userContextService)
        {
            _sliderPhotoService = sliderPhotoService;
            _logger = logger;
            _userContextService = userContextService;
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
        [HttpPost]
        [Authorize(Roles = "Moderator,Administrator,Developer")]
        public ActionResult UploadPhoto([FromForm] UploadSliderPhotoDto form)
        {
            int id = _sliderPhotoService.UploadPhoto(form);
            var log = $"Użytkownik {_userContextService.GetUserName}, userID {_userContextService.GetUserId} uploaduje nowe zdjęcie id={id} do slidera.";
            _logger.LogTrace(log);
            return Created($"/api/slider-Photo/{id}", null);
        }
    }
}
