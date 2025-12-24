using AutoTvde.Domain.Common;
using AutoTvde.Domain.Enums;

namespace AutoTvde.Domain.Entities;

public class Vehicle : BaseEntity
{
    public string LicensePlate { get; set; } = default!;
    public string Make { get; set; } = default!;
    public string Model { get; set; } = default!;
    public int PowerKw { get; set; }
    public int Year { get; set; }
    public VehicleUsage Usage { get; set; }
}