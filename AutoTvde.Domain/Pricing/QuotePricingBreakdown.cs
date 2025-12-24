namespace AutoTvde.Domain.Pricing;

public class QuotePricingBreakdown
{
    public decimal BasePremium { get; set; }

    public decimal AgeAdjustment { get; set; }
    public decimal UsageAdjustment { get; set; }
    public decimal CitySurcharge { get; set; }
    public decimal NcbDiscount { get; set; }

    public decimal OptionalCoverages { get; set; }

    public decimal Total { get; set; }
}