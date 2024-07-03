using AutoMapper;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;

namespace Explorer.Tours.Core.Mappers;

public class ToursProfile : Profile
{
    public ToursProfile()
    {
        CreateMap<EquipmentDto, Equipment>().ReverseMap();

        CreateMap<CreateTourDto, Tour>()
            .ForMember(dest => dest.KeyPoints, opt => opt.MapFrom(src => src.KeyPoints));

        CreateMap<CreateKeyPointDto, KeyPoint>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => new Image(src.Image.Filename, Convert.FromBase64String(src.Image.Data))));

        CreateMap<Tour, CreateTourDto>()
            .ForMember(dest => dest.KeyPoints, opt => opt.MapFrom(src => src.KeyPoints));

        CreateMap<KeyPoint, CreateKeyPointDto>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => new ImageDto { Filename = src.Image.Filename, Data = Convert.ToBase64String(src.Image.Data) }));

        CreateMap<TourDto, Tour>()
            .ForMember(dest => dest.KeyPoints, opt => opt.MapFrom(src => src.KeyPoints));

        CreateMap<Tour, TourDto>()
            .ForMember(dest => dest.KeyPoints, opt => opt.MapFrom(src => src.KeyPoints));

        CreateMap<KeyPointDto, KeyPoint>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => new Image(src.Image.Filename, Convert.FromBase64String(src.Image.Data))));

        CreateMap<KeyPoint, KeyPointDto>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => new ImageDto { Filename = src.Image.Filename, Data = Convert.ToBase64String(src.Image.Data) }));

        CreateMap<ImageDto, Image>()
            .ConstructUsing(dto => new Image(dto.Filename, Convert.FromBase64String(dto.Data)));

        CreateMap<Image, ImageDto>()
            .ForMember(dto => dto.Data, opt => opt.MapFrom(image => Convert.ToBase64String(image.Data)));
    }
}