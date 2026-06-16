using MediatR;
using O2morny.Application.Features.Auth.DTOs;

namespace O2morny.Application.Features.Auth
{
    public class GetRolesQuery : IRequest<List<RoleDto>>
    {
    }
}
