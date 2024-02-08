using Microsoft.EntityFrameworkCore;


public class KundeDb : DbContext
{
    public DbSet<Kunde> kunden { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {   //TODO serv config aus app json holen
        optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=admin");
    }
}


