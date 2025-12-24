using AutoTvde.Domain.Entities;
using AutoTvde.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

[Authorize]
[ApiController]
[Route("api/clients")]
public class ClientsController : ControllerBase
{
    private readonly AutoTvdeDbContext _db;

    public ClientsController(AutoTvdeDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<ActionResult<ClientDto>> Create(ClientCreateDto dto)
    {
        var age = DateTime.UtcNow.Year - dto.BirthDate.Year;
        if (age < 18)
            return ValidationProblem("Client must be at least 18 years old.");

        var entity = new Client
        {
            Name = dto.Name,
            Email = dto.Email,
            Nif = dto.Nif,
            BirthDate = dto.BirthDate
        };

        _db.Clients.Add(entity);
        await _db.SaveChangesAsync();

        var result = new ClientDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Email = entity.Email,
            Nif = entity.Nif,
            BirthDate = entity.BirthDate
        };

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
    }

    [HttpGet]
    public async Task<ActionResult<PagedResultDto<ClientDto>>> GetAll(
        int page = 1,
        int pageSize = 10)
    {
        if (page <= 0 || pageSize <= 0)
            return BadRequest("Page and PageSize must be greater than zero.");

        var query = _db.Clients.AsNoTracking();

        var total = await query.CountAsync();

        var items = await query
            .OrderBy(x => x.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new ClientDto
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Nif = x.Nif,
                BirthDate = x.BirthDate
            })
            .ToListAsync();

        return Ok(new PagedResultDto<ClientDto>
        {
            Items = items,
            Total = total,
            Page = page,
            PageSize = pageSize
        });
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ClientDto>> GetById(Guid id)
    {
        var client = await _db.Clients
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new ClientDto
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Nif = x.Nif,
                BirthDate = x.BirthDate
            })
            .FirstOrDefaultAsync();

        if (client is null)
            return NotFound();

        return Ok(client);
    }
}
