using AutoMapper;
using ProductCatalog.API.Models;

namespace ProductCatalog.API
{
    public class ModelProfile : Profile
    {
        public ModelProfile()
        {
            CreateMap<Brand, Infrastructure.Entities.Brand>().ReverseMap();
            CreateMap<BrandCreate, Infrastructure.Entities.Brand>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => Guid.CreateVersion7()));
        }
    }
}
