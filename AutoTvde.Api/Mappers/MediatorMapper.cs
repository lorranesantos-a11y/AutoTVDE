using AutoTvde.Api.Dtos.Mediators;
using AutoTvde.Domain.Entities;

namespace AutoTvde.Api.Mappers;

public static class MediatorMapper
{
    public static Mediator ToEntity(CreateMediatorRequestDto dto)
    {
        return new Mediator
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Email = dto.Email,
            Tier = dto.Tier,
            CommissionRate = dto.CommissionRate
        };
    }

    public static MediatorResponseDto ToDto(Mediator mediator)
    {
        return new MediatorResponseDto
        {
            Id = mediator.Id,
            Name = mediator.Name,
            Email = mediator.Email,
            Tier = mediator.Tier,
            CommissionRate = mediator.CommissionRate
        };
    }
}
