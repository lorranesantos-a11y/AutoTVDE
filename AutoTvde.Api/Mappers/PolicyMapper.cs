using AutoTvde.Domain.Entities;

namespace AutoTvde.Api.Mappers;

public static class PolicyMapper
{
    public static PolicyDto ToDto(Policy policy)
    {
        return new PolicyDto
        {
            Id = policy.Id,
            PolicyNumber = policy.PolicyNumber,
            EffectiveFrom = policy.EffectiveFrom,
            EffectiveTo = policy.EffectiveTo,
            TotalPremium = policy.TotalPremium,
            Commission = policy.Commission
        };
    }
}
