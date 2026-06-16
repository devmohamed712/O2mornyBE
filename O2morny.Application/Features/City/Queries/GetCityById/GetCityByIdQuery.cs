using MediatR;

namespace O2morny.Application.Features.City
{
    public class GetCityByIdQuery : IRequest<CityDto>
    {
        public int Id { get; set; }
    }
}
