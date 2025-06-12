using AutoMapper;
using Demo.Domain.AggregatesModel.Company2Aggregate;
using Demo.Domain.AggregatesModel.ProductAggregate;

namespace Demo.API.DTOs.Profiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductType, ProductTypeDto>().ReverseMap();
            CreateMap<Company2, CompanyDto>().ReverseMap();
            CreateMap<Address2, AddressDto>().ReverseMap();

        }
    }
}
