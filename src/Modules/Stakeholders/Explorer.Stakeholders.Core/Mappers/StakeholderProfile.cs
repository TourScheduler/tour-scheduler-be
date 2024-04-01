using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;

namespace Explorer.Stakeholders.Core.Mappers;

public class StakeholderProfile : Profile
{
    public StakeholderProfile()
    {
        CreateMap<TouristInterestDto, Interest>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Interest))
            .ConstructUsing(src => new Interest((Domain.InterestType)src.Interest));

        CreateMap<Interest, TouristInterestDto>()
            .ForMember(dest => dest.Interest, opt => opt.MapFrom(src => src.Type));
    }
}