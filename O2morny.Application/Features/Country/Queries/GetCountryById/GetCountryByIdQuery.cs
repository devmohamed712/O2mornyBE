using MediatR;

namespace O2morny.Application.Features.Country
{
    public class GetCountryByIdQuery : IRequest<CountryDto>
    {
        public int Id { get; set; }
    }
}
