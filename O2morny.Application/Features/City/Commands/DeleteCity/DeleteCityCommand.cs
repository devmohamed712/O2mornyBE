using MediatR;

namespace O2morny.Application.Features.City
{
    public class DeleteCityCommand : IRequest
    {
        public int Id { get; set; }
    }
}
