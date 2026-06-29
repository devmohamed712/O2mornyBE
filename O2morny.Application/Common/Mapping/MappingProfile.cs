using AutoMapper;
using O2morny.Application.Features;
using O2morny.Application.Features.Account;
using O2morny.Application.Features.City;
using O2morny.Application.Features.Country;
using O2morny.Domain.Common.Entities;

namespace O2morny.Application.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Country, CountryDto>();
            CreateMap<City, CityDto>();
            CreateMap<Account, AccountDto>();
            CreateMap<ServiceProviderReview, ServiceProviderReviewDto>();
        }
    }
}
