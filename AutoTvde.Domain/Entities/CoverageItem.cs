using AutoTvde.Domain.Common;
using AutoTvde.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTvde.Domain.Entities;

public class CoverageItem : BaseEntity
{
    public Guid QuoteId { get; set; }

    public CoverageCode CoverageCode { get; set; }

    public decimal? Limit { get; set; }
    public decimal? Deductible { get; set; }

    public decimal PremiumPart { get; set; }
}

