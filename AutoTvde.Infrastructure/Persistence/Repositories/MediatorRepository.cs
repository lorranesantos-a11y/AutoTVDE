using AutoTvde.Domain.Entities;
using AutoTvde.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AutoTvde.Infrastructure.Persistence.Repositories;

public class MediatorRepository : IMediatorRepository
{
    private readonly AutoTvdeDbContext _context;

    public MediatorRepository(AutoTvdeDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Mediator mediator)
    {
        await _context.Mediators.AddAsync(mediator);
    }

    public async Task<IEnumerable<Mediator>> GetAllAsync()
    {
        return await _context.Mediators
            .AsNoTracking()
            .OrderBy(m => m.Name)
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
