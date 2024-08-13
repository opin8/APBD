using System.ComponentModel.DataAnnotations;

namespace PrzykladowyEgzamin.Models;

public class ClientCategory
{
    [Key]
    public int IdClientCategory { get; set; }
    
    [MaxLength(100)]
    public string Name { get; set; }

    public int DiscountPerc { get; set; }
    
}