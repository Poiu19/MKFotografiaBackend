using AutoMapper;
using MKFotografiaBackend.Entities;
using MKFotografiaBackend.Models;

namespace MKFotografiaBackend.Components
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<SliderPhoto, SliderPhotoDto>();
        }
    }
}
