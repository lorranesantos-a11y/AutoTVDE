using AutoTvde.Domain.Common;
using AutoTvde.Domain.Enums;


namespace AutoTvde.Domain.Entities;

public class Quote : BaseEntity
{
    public string Number { get; set; } = default!;

    public Guid ClientId { get; set; }
    public Guid VehicleId { get; set; }
    public Guid? MediatorId { get; set; }

    public decimal BasePremium { get; set; }
    public decimal Surcharges { get; set; }
    public decimal Discounts { get; set; }
    public decimal TotalPremium { get; set; }

    public QuoteStatus Status { get; set; }

    public List<CoverageItem> CoverageItems { get; set; } = new();
}
