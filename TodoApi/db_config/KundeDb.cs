using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


public class KundeDb : DbContext
{
    private IConfiguration config;
    public KundeDb(IConfiguration configuration)
    {
        config = configuration;
    }
    public DbSet<Kunde> kunden { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var conn = config.GetConnectionString("DefaultConnection");
        optionsBuilder.UseNpgsql(conn);
    }
}



