using AutoTvde.Domain.Enums;

namespace AutoTvde.Domain.Pricing;

public class QuotePricingInput
{
    public DateTime BirthDate { get; set; }
    public int VehiclePowerKw { get; set; }
    public VehicleUsage VehicleUsage { get; set; }

    public string City { get; set; } = default!;
    public int NcbYears { get; set; }

    public bool IncludeGlass { get; set; }
    public bool IncludeRoadside { get; set; }
}