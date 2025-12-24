public class PolicyResponseDto
{
    public Guid PolicyId { get; set; }
    public string PolicyNumber { get; set; } = default!;
    public DateTime EffectiveFrom { get; set; }
    public DateTime EffectiveTo { get; set; }
    public decimal TotalPremium { get; set; }
    public decimal Commission { get; set; }
}