using AutoTvde.Api.Dtos.Vehicles;
using AutoTvde.Api.Mappers;
using AutoTvde.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoTvde.Api.Controllers;

[Authorize(Roles = "Admin,Mediator")]
[ApiController]
[Route("api/vehicles")]
public class VehiclesController : ControllerBase
{
    private readonly IVehicleRepository _repository;

    public VehiclesController(IVehicleRepository repository)
    {
        _repository = repository;
    }

    // POST /api/vehicles
    [HttpPost]
    public async Task<IActionResult> Create(CreateVehicleRequestDto request)
    {
        if (request.Year < 2000)
            return BadRequest("Vehicle year must be >= 2000.");

        var vehicle = VehicleMapper.ToEntity(request);

        await _repository.AddAsync(vehicle);
        await _repository.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetPaged),
            new { id = vehicle.Id },
            VehicleMapper.ToDto(vehicle)
        );
    }

    // GET /api/vehicles?page=&pageSize=
    [HttpGet]
    public async Task<IActionResult> GetPaged(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        if (page <= 0 || pageSize <= 0)
            return BadRequest("Invalid paging parameters.");

        var (items, total) = await _repository.GetPagedAsync(page, pageSize);

        return Ok(new
        {
            page,
            pageSize,
            total,
            items = items.Select(VehicleMapper.ToDto)
        });
    }
}
