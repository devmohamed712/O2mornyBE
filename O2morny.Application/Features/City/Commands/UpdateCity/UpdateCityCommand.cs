using MediatR;

namespace O2morny.Application.Features.City
{
    public class UpdateCityCommand : IRequest<CityDto>
    {
        public int Id { get; set; }
        public string ArName { get; set; }
        public string EnName { get; set; }
        public int CountryId { get; set; }
    }
}
