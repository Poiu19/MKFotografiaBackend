using AutoMapper;
using MKFotografiaBackend.Entities;
using MKFotografiaBackend.Models.Outgoing;

namespace MKFotografiaBackend.Components
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<SliderPhoto, SliderPhotoDto>();
            CreateMap<Role, RoleDto>();
            CreateMap<User, UserDto>();
        }
    }
}
