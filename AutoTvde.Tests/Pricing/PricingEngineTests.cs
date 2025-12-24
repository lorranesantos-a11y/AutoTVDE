using AutoTvde.Domain.Enums;
using AutoTvde.Domain.Pricing;
using FluentAssertions;
using Xunit;

namespace AutoTvde.Tests.Pricing;

public class PricingEngineTests
{
    private readonly IPricingEngine _engine = new PricingEngine();

    [Theory]
    [InlineData(80, 220)]
    [InlineData(90, 290)]
    [InlineData(150, 360)]
    public void Calculate_BasePremium_ByPowerKw(int powerKw, decimal expectedBase)
    {
        var input = CreateValidInput(powerKw: powerKw);

        var result = _engine.Calculate(input);

        result.BasePremium.Should().Be(expectedBase);
    }

    [Theory]
    [InlineData(20, 0.25)] // <25 → +25%
    [InlineData(40, 0.00)] // 25–60 → 0%
    [InlineData(65, 0.15)] // >60 → +15%
    public void Calculate_AgeAdjustment_IsCorrect(int age, decimal expectedRate)
    {
        var birthDate = DateTime.UtcNow.AddYears(-age);
        var input = CreateValidInput(birthDate: birthDate);

        var result = _engine.Calculate(input);

        result.AgeAdjustment.Should().Be(result.BasePremium * expectedRate);
    }

    [Fact]
    public void Calculate_CitySurcharge_Lisboa_ShouldApply5Percent()
    {
        var input = CreateValidInput(city: "Lisboa");

        var result = _engine.Calculate(input);

        result.CitySurcharge.Should().BeGreaterThan(0);
    }

    [Fact]
    public void Calculate_CitySurcharge_OtherCity_ShouldBeZero()
    {
        var input = CreateValidInput(city: "Coimbra");

        var result = _engine.Calculate(input);

        result.CitySurcharge.Should().Be(0);
    }

    [Fact]
    public void Calculate_NcbDiscount_ForFiveYears_ShouldBe15Percent()
    {
        var input = CreateValidInput(ncbYears: 5);

        var result = _engine.Calculate(input);

        result.NcbDiscount.Should().BeNegative();
    }

    [Fact]
    public void Calculate_OptionalCoverages_GlassAndRoadside()
    {
        var input = CreateValidInput(includeGlass: true, includeRoadside: true);

        var result = _engine.Calculate(input);

        result.OptionalCoverages.Should().Be(55);
    }

    [Fact]
    public void Calculate_FullScenario_ShouldMatchExpectedTotal()
    {
        var input = new QuotePricingInput
        {
            BirthDate = DateTime.UtcNow.AddYears(-30),
            VehiclePowerKw = 100,
            VehicleUsage = VehicleUsage.TVDE,
            City = "Lisboa",
            NcbYears = 5,
            IncludeGlass = true,
            IncludeRoadside = true
        };

        var result = _engine.Calculate(input);

        result.Total.Should().Be(344.88m);
    }

    private static QuotePricingInput CreateValidInput(
        int powerKw = 80,
        DateTime? birthDate = null,
        string city = "Porto",
        int ncbYears = 0,
        bool includeGlass = false,
        bool includeRoadside = false)
    {
        return new QuotePricingInput
        {
            BirthDate = birthDate ?? DateTime.UtcNow.AddYears(-30),
            VehiclePowerKw = powerKw,
            VehicleUsage = VehicleUsage.TVDE,
            City = city,
            NcbYears = ncbYears,
            IncludeGlass = includeGlass,
            IncludeRoadside = includeRoadside
        };
    }
}
