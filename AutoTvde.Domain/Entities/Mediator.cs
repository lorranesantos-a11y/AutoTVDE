using AutoTvde.Domain.Common;
using AutoTvde.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace AutoTvde.Domain.Entities;

public class Mediator : BaseEntity
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public MediatorTier Tier { get; set; }
    public decimal CommissionRate { get; set; }
}
