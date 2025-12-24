using AutoTvde.Domain.Entities;

namespace AutoTvde.Infrastructure.Persistence.Repositories.Interfaces;

public interface IVehicleRepository
{
    Task AddAsync(Vehicle vehicle);
    Task<(IEnumerable<Vehicle> Items, int Total)> GetPagedAsync(int page, int pageSize);
    Task SaveChangesAsync();
}
