using AutoMapper;
using Skinet.Api.Dtos;
using Skinet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skinet.Api.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(dst => dst.ProductBrand, opt => opt.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dst => dst.ProductType, opt => opt.MapFrom(src => src.ProductType.Name))
                .ForMember(dst => dst.PictureUrl, opt => opt.MapFrom<ProductUrlResolver>());
        }
    }
}
