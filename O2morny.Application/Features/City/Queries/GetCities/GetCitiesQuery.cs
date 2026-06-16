using MediatR;

namespace O2morny.Application.Features.City
{
    public class GetCitiesQuery : IRequest<List<CityDto>>
    {
        public int CountryId { get; set; }
    }
}
