using System.ComponentModel.DataAnnotations;

namespace PrzykladowyEgzamin.Models;

public class BoatStandard
{
    [Key]
    public int IdBoatStandard { get; set; }

    [MaxLength(100)]
    [Required]
    public string Name { get; set; }

    [Range(1,3, ErrorMessage = "Level must be between 1 and 3.")]
    public int Level { get; set; }
    
}