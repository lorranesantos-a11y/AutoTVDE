using AutoTvde.Domain.Enums;

public class CreateQuoteRequestDto
{
    public DateTime BirthDate { get; set; }

    public int VehiclePowerKw { get; set; }
    public VehicleUsage VehicleUsage { get; set; }

    public string City { get; set; } = string.Empty;

    public int NcbYears { get; set; }

    public bool HasGlassCoverage { get; set; }
    public bool HasRoadsideCoverage { get; set; }
}
