using AutoMapper;
using Payment_API.src.DTOs;
using Payment_API.src.Models;

namespace Payment_API.src.Mapping
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            CreateMap<NewSaleDTO, Sale>()
            .ForMember(dest => dest.Seller, opt => opt.MapFrom(src => src.Seller))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Created, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .AfterMap((_, dest) =>
            {
                dest.Created = DateTime.Now;
                dest.Status = EnumStatus.Aguardando;
            });

            CreateMap<EnumStatusUpdateDTO, EnumStatus>();
        }


    }
}