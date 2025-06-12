using AutoMapper;
using Demo.Domain.AggregatesModel.ProductAggregate;

namespace Demo.API.DTOs.Profiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductType, ProductTypeDto>().ReverseMap();
        }
    }
}
