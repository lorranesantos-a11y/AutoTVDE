using AutoTvde.Domain.Entities;
using AutoTvde.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AutoTvde.Infrastructure.Persistence.Repositories;

public class VehicleRepository : IVehicleRepository
{
    private readonly AutoTvdeDbContext _context;

    public VehicleRepository(AutoTvdeDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Vehicle vehicle)
    {
        await _context.Vehicles.AddAsync(vehicle);
    }

    public async Task<(IEnumerable<Vehicle> Items, int Total)> GetPagedAsync(
        int page,
        int pageSize)
    {
        var query = _context.Vehicles.AsNoTracking();

        var total = await query.CountAsync();

        var items = await query
            .OrderBy(v => v.LicensePlate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, total);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
