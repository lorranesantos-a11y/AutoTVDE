using AutoTvde.Api.Dtos.Mediators;
using AutoTvde.Api.Mappers;
using AutoTvde.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoTvde.Api.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/mediators")]
public class MediatorsController : ControllerBase
{
    private readonly IMediatorRepository _repository;

    public MediatorsController(IMediatorRepository repository)
    {
        _repository = repository;
    }

    // POST /api/mediators
    [HttpPost]
    public async Task<IActionResult> Create(CreateMediatorRequestDto request)
    {
        var mediator = MediatorMapper.ToEntity(request);

        await _repository.AddAsync(mediator);
        await _repository.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetAll),
            new { id = mediator.Id },
            MediatorMapper.ToDto(mediator)
        );
    }

    // GET /api/mediators
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var mediators = await _repository.GetAllAsync();

        return Ok(mediators.Select(MediatorMapper.ToDto));
    }
}
