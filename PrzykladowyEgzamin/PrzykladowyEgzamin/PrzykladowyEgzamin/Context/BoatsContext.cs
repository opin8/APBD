using Microsoft.EntityFrameworkCore;
using PrzykladowyEgzamin.Models;

namespace PrzykladowyEgzamin.Context;

public class BoatsContext : DbContext
{
    public BoatsContext()
    {
    }

    public BoatsContext(DbContextOptions<BoatsContext> options)
        : base(options)
    {
    }
    
    public DbSet<Sailboat> Sailboats { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<ClientCategory> ClientCategories { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<BoatStandard> BoatStandards { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Reservation>()
            .Property(r => r.Price)
            .HasColumnType("decimal(18, 2)");

        modelBuilder.Entity<Sailboat>()
            .Property(s => s.Price)
            .HasColumnType("decimal(18, 2)");
    }

}