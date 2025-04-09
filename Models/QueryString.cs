using System.ComponentModel.DataAnnotations;

namespace minimal_validation.Models;

public class QueryString
{
    [Required]
    [MaxLength(10)]
    public string QueryParam { get; set; }
}