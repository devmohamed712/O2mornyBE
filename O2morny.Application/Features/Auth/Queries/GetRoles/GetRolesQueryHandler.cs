using MediatR;
using O2morny.Application.Common.Interfaces.Services;
using O2morny.Application.Features.Auth.DTOs;

namespace O2morny.Application.Features.Auth
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, List<RoleDto>>
    {
        private readonly IAuthService _authService;

        public GetRolesQueryHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<List<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            return await _authService.GetRoles();
        }
    }
}
