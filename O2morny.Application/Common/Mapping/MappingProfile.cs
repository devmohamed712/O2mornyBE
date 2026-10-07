using AutoMapper;
using O2morny.Application.Features.Account;
using O2morny.Application.Features.City;
using O2morny.Application.Features.Country;
using O2morny.Application.Features.Shop;
using O2morny.Domain.Common.Entities;

namespace O2morny.Application.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Country, CountryDto>();
            CreateMap<City, CityDto>();
            CreateMap<Account, AccountDto>()
            .ForMember(
                dto => dto.CountryId,
                opt => opt.MapFrom(account => account.City.CountryId)
            );
            CreateMap<Shop, ShopDto>()
            .ForMember(
            dto => dto.ShopImages,
            opt => opt.MapFrom(shop => shop.Images)
            )
            .ForMember(
            dto => dto.ShopWorkingHours,
            opt => opt.MapFrom(shop => shop.WorkingHours)
            );
            CreateMap<ShopImage, ShopImageDto>();
            CreateMap<ShopWorkingHour, ShopWorkingHourDto>();
        }
    }
}
