using System.ComponentModel.DataAnnotations;

namespace minimal_validation.Models;

public class RequestBody
{
    [Required]
    [MaxLength(10)]
    public string Body { get; set; }
}