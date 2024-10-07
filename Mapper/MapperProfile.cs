using AutoMapper;
using seminar1._1.Dto;
using seminar1._1.Models;

namespace seminar1._1.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductGroup, ProductGroupDto>().ReverseMap();
        }
    }
}
