using AutoTvde.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace AutoTvde.Api.Dtos.Mediators;

public class CreateMediatorRequestDto
{
    [Required]
    public string Name { get; set; } = default!;

    [Required, EmailAddress]
    public string Email { get; set; } = default!;

    [Required]
    public MediatorTier Tier { get; set; }

    [Range(0, 1)]
    public decimal CommissionRate { get; set; }
}
