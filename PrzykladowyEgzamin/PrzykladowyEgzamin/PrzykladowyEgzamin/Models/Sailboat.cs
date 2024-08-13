using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace PrzykladowyEgzamin.Models;

public class Sailboat
{
    [Key]
    public int IdSailboat { get; set; }
    
    [MaxLength(100)]
    [Required]
    public string Name { get; set; }
    
    public int Capacity { get; set; }
    
    [MaxLength(100)]
    [Required]
    public string Description { get; set; }
    
    public decimal Price { get; set; }
    
    public int IdBoatStandard { get; set; }
    [ForeignKey(nameof(IdBoatStandard))]
    public BoatStandard BoatStandard { get; set; }
    
    public ICollection<Reservation> Reservations {get; set;} = new List<Reservation>();
}