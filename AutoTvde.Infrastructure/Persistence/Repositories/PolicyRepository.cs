using AutoTvde.Domain.Entities;
using AutoTvde.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace AutoTvde.Infrastructure.Persistence.Repositories;

public class PolicyRepository : IPolicyRepository
{
    private readonly AutoTvdeDbContext _context;

    public PolicyRepository(AutoTvdeDbContext context)
    {
        _context = context;
    }

    public async Task<Policy?> GetByIdAsync(Guid id)
    {
        return await _context.Policies
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}
