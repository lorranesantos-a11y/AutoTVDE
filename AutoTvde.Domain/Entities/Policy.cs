using AutoTvde.Domain.Common;

namespace AutoTvde.Domain.Entities;

public class Policy : BaseEntity
{
    public string PolicyNumber { get; set; } = default!;

    public Guid QuoteId { get; set; }

    public DateTime EffectiveFrom { get; set; }
    public DateTime EffectiveTo { get; set; }

    public decimal TotalPremium { get; set; }
    public decimal Commission { get; set; }

    public DateTime IssuedAt { get; set; }
}

