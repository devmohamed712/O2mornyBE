using MediatR;

namespace O2morny.Application.Features.Country
{
    public class CreateCountryCommand : IRequest<CountryDto>
    {
        public string ArName { get; set; }
        public string EnName { get; set; }
    }
}
