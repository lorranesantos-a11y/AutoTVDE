using AutoTvde.Domain.Enums;

namespace AutoTvde.Domain.Pricing;

public class PricingEngine : IPricingEngine
{
    public QuotePricingBreakdown Calculate(QuotePricingInput input)
    {
        var breakdown = new QuotePricingBreakdown();

        breakdown.BasePremium = input.VehiclePowerKw switch
        {
            <= 80 => 220m,
            <= 110 => 290m,
            _ => 360m
        };

        var subtotal = breakdown.BasePremium;

        var age = CalculateAge(input.BirthDate);
        breakdown.AgeAdjustment = age switch
        {
            < 25 => subtotal * 0.25m,
            > 60 => subtotal * 0.15m,
            _ => 0m
        };
        subtotal += breakdown.AgeAdjustment;

        breakdown.UsageAdjustment =
            input.VehicleUsage == VehicleUsage.TVDE
                ? subtotal * 0.12m
                : 0m;

        subtotal += breakdown.UsageAdjustment;

        breakdown.CitySurcharge =
            IsMajorCity(input.City)
                ? subtotal * 0.05m
                : 0m;

        subtotal += breakdown.CitySurcharge;

        breakdown.NcbDiscount = CalculateNcbDiscount(input.NcbYears, subtotal);
        subtotal += breakdown.NcbDiscount;

        breakdown.OptionalCoverages = 0m;

        if (input.IncludeGlass)
            breakdown.OptionalCoverages += 35m;

        if (input.IncludeRoadside)
            breakdown.OptionalCoverages += 20m;

        subtotal += breakdown.OptionalCoverages;

        breakdown.Total = Math.Round(subtotal, 2, MidpointRounding.AwayFromZero);

        return breakdown;
    }

    private static int CalculateAge(DateTime birthDate)
    {
        var today = DateTime.UtcNow.Date;
        var age = today.Year - birthDate.Year;

        if (birthDate.Date > today.AddYears(-age))
            age--;

        return age;
    }

    private static bool IsMajorCity(string city)
        => city.Equals("Lisboa", StringComparison.OrdinalIgnoreCase)
        || city.Equals("Porto", StringComparison.OrdinalIgnoreCase);

    private static decimal CalculateNcbDiscount(int ncbYears, decimal subtotal)
    {
        var rate = ncbYears switch
        {
            0 => 0m,
            <= 2 => -0.05m,
            <= 4 => -0.10m,
            _ => -0.15m
        };

        return subtotal * rate;
    }
}
