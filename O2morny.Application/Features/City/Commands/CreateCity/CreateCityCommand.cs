using MediatR;

namespace O2morny.Application.Features.City
{
    public class CreateCityCommand : IRequest<CityDto>
    {
        public string ArName { get; set; }
        public string EnName { get; set; }
        public int CountryId { get; set; }
    }
}
