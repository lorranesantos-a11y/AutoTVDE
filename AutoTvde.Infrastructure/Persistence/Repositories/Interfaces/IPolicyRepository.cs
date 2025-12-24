using AutoTvde.Domain.Entities;

namespace AutoTvde.Infrastructure.Persistence.Repositories.Interfaces;

public interface IPolicyRepository
{
    Task<Policy?> GetByIdAsync(Guid id);
}
