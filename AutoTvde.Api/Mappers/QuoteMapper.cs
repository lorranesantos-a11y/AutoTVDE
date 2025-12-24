using AutoTvde.Domain.Entities;
using AutoTvde.Domain.Enums;
using AutoTvde.Domain.Pricing;

namespace AutoTvde.Api.Mappers;

public static class QuoteMapper
{
    public static QuotePricingInput ToPricingInput(CreateQuoteRequestDto dto)
    {
        return new QuotePricingInput
        {
            BirthDate = dto.BirthDate,
            VehiclePowerKw = dto.VehiclePowerKw,
            VehicleUsage = dto.VehicleUsage,

            City = dto.City,
            NcbYears = dto.NcbYears,

            IncludeGlass = dto.HasGlassCoverage,
            IncludeRoadside = dto.HasRoadsideCoverage
        };
    }

    public static QuoteResponseDto ToDto(QuotePricingBreakdown breakdown)
    {
        return new QuoteResponseDto
        {
            BasePremium = breakdown.BasePremium,

            AgeAdjustment = breakdown.AgeAdjustment,
            UsageAdjustment = breakdown.UsageAdjustment,
            CitySurcharge = breakdown.CitySurcharge,
            NcbDiscount = Math.Round(breakdown.NcbDiscount, 2),

            OptionalCoverages = breakdown.OptionalCoverages,

            Total = breakdown.Total
        };
    }

    public static Quote ToEntity(QuotePricingBreakdown breakdown)
    {
        return new Quote
        {
            Number = $"Q-{DateTime.UtcNow:yyyyMMddHHmmssfff}",
            Status = QuoteStatus.Priced,

            BasePremium = breakdown.BasePremium,
            TotalPremium = breakdown.Total,

            CreatedAt = DateTime.UtcNow
        };
    }
}
