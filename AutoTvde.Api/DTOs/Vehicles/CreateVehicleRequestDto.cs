using AutoTvde.Domain.Enums;

namespace AutoTvde.Api.Dtos.Vehicles;

public class CreateVehicleRequestDto
{
    public string LicensePlate { get; set; } = default!;
    public string Make { get; set; } = default!;
    public string Model { get; set; } = default!;
    public int Year { get; set; }
    public int PowerKw { get; set; }
    public VehicleUsage Usage { get; set; }
}
