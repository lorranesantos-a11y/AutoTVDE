using AutoTvde.Api.Mappers;
using AutoTvde.Domain.Entities;
using AutoTvde.Domain.Enums;
using AutoTvde.Domain.Pricing;
using AutoTvde.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize]
[ApiController]
[Route("api/quotes")]
[Authorize(Roles = "Admin,Mediator")]
public class QuotesController : ControllerBase
{
    private readonly IPricingEngine _pricingEngine;

    public QuotesController(IPricingEngine pricingEngine)
    {
        _pricingEngine = pricingEngine;
    }

    [HttpPost("price")]
    public async Task<IActionResult> Price(
        [FromBody] CreateQuoteRequestDto request,
        [FromServices] IPricingEngine pricingEngine,
        [FromServices] AutoTvdeDbContext db)
    {
        var age = DateTime.Today.Year - request.BirthDate.Year;
        if (request.BirthDate.Date > DateTime.Today.AddYears(-age))
            age--;

        if (age < 18)
            return BadRequest("Client must be at least 18 years old.");

        var pricingInput = QuoteMapper.ToPricingInput(request);
        var breakdown = pricingEngine.Calculate(pricingInput);

        var quote = QuoteMapper.ToEntity(breakdown);

        db.Quotes.Add(quote);
        await db.SaveChangesAsync();

        return Ok(new
        {
            quoteId = quote.Id,
            quoteNumber = quote.Number,
            breakdown = QuoteMapper.ToDto(breakdown)
        });
    }

    [HttpPost("{id:guid}/bind")]
    public async Task<IActionResult> Bind(
    Guid id,
    [FromServices] AutoTvdeDbContext db)
    {
        var quote = await db.Quotes.FirstOrDefaultAsync(q => q.Id == id);

        if (quote is null)
            return NotFound("Quote not found.");

        if (quote.Status != QuoteStatus.Priced)
            return BadRequest("Only priced quotes can be bound.");

        var policy = new Policy
        {
            PolicyNumber = $"P-{DateTime.UtcNow:yyyyMMddHHmmssfff}",

            QuoteId = quote.Id,

            EffectiveFrom = DateTime.UtcNow,
            EffectiveTo = DateTime.UtcNow.AddYears(1),

            TotalPremium = quote.TotalPremium,

            Commission = Math.Round(quote.TotalPremium * 0.10m, 2),

            IssuedAt = DateTime.UtcNow
        };

        quote.Status = QuoteStatus.Bound;

        db.Policies.Add(policy);
        await db.SaveChangesAsync();

        return Ok(PolicyMapper.ToDto(policy));
    }
}
