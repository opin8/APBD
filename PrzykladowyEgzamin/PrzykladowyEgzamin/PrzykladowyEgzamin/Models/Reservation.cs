using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace PrzykladowyEgzamin.Models;

public class Reservation
{
    
    [Key]
    public int IdReservation { get; set; }
    
    [Column(TypeName = "date")]
    public DateOnly DateTo { get; set; }
    [Column(TypeName = "date")]
    public DateOnly DateFrom { get; set; }
    
    public int Capacity { get; set; }
    public int NumOfBoats { get; set; }
    public bool? Fulfilled { get; set; }
    public decimal? Price { get; set; }
    [MaxLength(100)]
    public string? CancelReason { get; set; }
    
    public int IdClient { get; set; }
    [ForeignKey(nameof(IdClient))]
    public ICollection<Client> Clients {get; set;} = new List<Client>();
    
    public int IdBoatStandard { get; set; }
    [ForeignKey(nameof(IdBoatStandard))]
    public BoatStandard BoatStandard { get; set; }


    
}