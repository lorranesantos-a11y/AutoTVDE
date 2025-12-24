using AutoTvde.Domain.Enums;

namespace AutoTvde.Api.Dtos.Mediators;

public class MediatorResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public MediatorTier Tier { get; set; }
    public decimal CommissionRate { get; set; }
}
