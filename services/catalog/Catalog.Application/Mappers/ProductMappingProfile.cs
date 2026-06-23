using AutoMapper;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Mappers
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile() 
        {
            CreateMap<ProductBrand, BrandResponseDto>().ReverseMap();
            CreateMap<Product, ProductResponseDto>().ReverseMap();
            CreateMap<ProductType, ProductTypeResponseDto>().ReverseMap();
        }
    }
}
