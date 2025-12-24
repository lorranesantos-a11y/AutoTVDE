using System.ComponentModel.DataAnnotations;

public class ClientCreateDto
{
    [Required, StringLength(120, MinimumLength = 3)]
    public string Name { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    [Required, RegularExpression(@"^\d{9}$")]
    public string Nif { get; set; }

    [Required]
    public DateTime BirthDate { get; set; }
}
