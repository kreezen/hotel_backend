using Microsoft.EntityFrameworkCore;

public class KundeDb : DbContext
{
    // Add DbSet properties for your entities
    public DbSet<Kunde> kunden { get; set; }
    


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Configure your local PostgreSQL connection string here
        optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=admin");
    }
}


