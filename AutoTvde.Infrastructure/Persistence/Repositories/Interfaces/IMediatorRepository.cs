using AutoTvde.Domain.Entities;

namespace AutoTvde.Infrastructure.Persistence.Repositories.Interfaces;

public interface IMediatorRepository
{
    Task AddAsync(Mediator mediator);
    Task<IEnumerable<Mediator>> GetAllAsync();
    Task SaveChangesAsync();
}
