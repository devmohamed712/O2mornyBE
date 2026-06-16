using MediatR;

namespace O2morny.Application.Features.Country
{
    public class GetCountriesQuery : IRequest<List<CountryDto>>
    {
    }
}
