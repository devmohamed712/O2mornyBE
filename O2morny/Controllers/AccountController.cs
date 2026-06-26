using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using O2morny.API.Models.Account;
using O2morny.Application.Common.Models;
using O2morny.Application.Features.Account;
using System.Security.Claims;

namespace O2morny.API.Controllers;

[ApiController]
[Route("api/account")]
[Authorize]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAccountsQuery(), ct);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAccountByIdQuery
        {
            Id = id
        }, ct);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateAccountRequest request, CancellationToken ct)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        var command = new CreateAccountCommand
        {
            Id = userId,
            Name = request.Name,
            NationalId = request.NationalId,
            DateOfBirth = request.DateOfBirth,
            HideBirthDate = request.HideBirthDate,
            CityId = request.CityId,
            Address = request.Address,
            IsAcceptTerms = request.IsAcceptTerms,
            IsAcceptPrivacy = request.IsAcceptPrivacy,
            Role = request.Role,
            ServiceProviderExperienceYears = request.ServiceProviderExperienceYears,
            ServiceProviderDescription = request.ServiceProviderDescription,

            NationalIdPictureFile =
                request.NationalIdPictureFile != null
                    ? new FileModel
                    {
                        FileName = request.NationalIdPictureFile.FileName,
                        FileStream = request.NationalIdPictureFile.OpenReadStream()
                    }
                    : null,

            ProfilePictureFile =
                request.ProfilePictureFile != null
                    ? new FileModel
                    {
                        FileName = request.ProfilePictureFile.FileName,
                        FileStream = request.ProfilePictureFile.OpenReadStream()
                    }
                    : null
        };

        var account = await _mediator.Send(command, ct);

        return Ok(account);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromForm] UpdateAccountRequest request, CancellationToken ct)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        var command = new UpdateAccountCommand
        {
            Id = userId,
            Name = request.Name,
            DateOfBirth = request.DateOfBirth,
            HideBirthDate = request.HideBirthDate,
            CityId = request.CityId,
            Address = request.Address,
            Role = request.Role,
            ServiceProviderExperienceYears = request.ServiceProviderExperienceYears,
            ServiceProviderDescription = request.ServiceProviderDescription,

            ProfilePictureFile =
                request.ProfilePictureFile != null
                    ? new FileModel
                    {
                        FileName = request.ProfilePictureFile.FileName,
                        FileStream = request.ProfilePictureFile.OpenReadStream()
                    }
                    : null
        };

        var account = await _mediator.Send(command, ct);

        return Ok(account);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken ct)
    {
        await _mediator.Send(new DeleteAccountCommand
        {
            Id = id
        }, ct);

        return NoContent();
    }
}