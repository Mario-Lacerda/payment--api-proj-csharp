using AutoMapper;
using Payment_API.src.DTOs;
using Payment_API.src.Extensions;
using Payment_API.src.Models;

namespace Payment_API.src.Mapping
{
    public class ModelToDtoProfile : Profile
    {
        public ModelToDtoProfile()
        {
            CreateMap<Sale, SaleDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.GetDisplayName()))
                .ForMember(dest => dest.Seller, opt => opt.MapFrom(src => src.Seller));

            CreateMap<Seller, SellerDTO>().ReverseMap();
        }
    }
}