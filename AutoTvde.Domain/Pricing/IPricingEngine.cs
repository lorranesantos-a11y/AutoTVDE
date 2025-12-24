namespace AutoTvde.Domain.Pricing;

public interface IPricingEngine
{
    QuotePricingBreakdown Calculate(QuotePricingInput input);
}