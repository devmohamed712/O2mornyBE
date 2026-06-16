using MediatR;

namespace O2morny.Application.Features.Country
{
    public class DeleteCountryCommand : IRequest
    {
        public int Id { get; set; }
    }
}
