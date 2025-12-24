using AutoTvde.Api.Dtos.Vehicles;
using AutoTvde.Domain.Entities;

namespace AutoTvde.Api.Mappers;

public static class VehicleMapper
{
    public static Vehicle ToEntity(CreateVehicleRequestDto dto)
    {
        return new Vehicle
        {
            Id = Guid.NewGuid(),
            LicensePlate = dto.LicensePlate,
            Make = dto.Make,
            Model = dto.Model,
            Year = dto.Year,
            PowerKw = dto.PowerKw,
            Usage = dto.Usage
        };
    }

    public static VehicleResponseDto ToDto(Vehicle vehicle)
    {
        return new VehicleResponseDto
        {
            Id = vehicle.Id,
            LicensePlate = vehicle.LicensePlate,
            Make = vehicle.Make,
            Model = vehicle.Model,
            Year = vehicle.Year,
            PowerKw = vehicle.PowerKw,
            Usage = vehicle.Usage
        };
    }
}
