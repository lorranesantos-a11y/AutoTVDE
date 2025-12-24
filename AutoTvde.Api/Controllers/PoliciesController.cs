using AutoTvde.Api.Mappers;
using AutoTvde.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoTvde.Api.Controllers;

[Authorize(Roles = "Admin,Mediator")]
[ApiController]
[Route("api/policies")]
public class PoliciesController : ControllerBase
{
    private readonly IPolicyRepository _repository;

    public PoliciesController(IPolicyRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var policy = await _repository.GetByIdAsync(id);

        if (policy is null)
            return NotFound();

        var dto = new PolicyResponseDto
        {
            PolicyId = policy.Id,
            PolicyNumber = policy.PolicyNumber,
            EffectiveFrom = policy.EffectiveFrom,
            EffectiveTo = policy.EffectiveTo,
            TotalPremium = policy.TotalPremium,
            Commission = policy.Commission
        };

        return Ok(dto);
    }
}
