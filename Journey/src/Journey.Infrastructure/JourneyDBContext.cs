using Microsoft.EntityFrameworkCore;
using Journey.Infrastructure.Entities;

namespace Journey.Infrastructure;
public class JourneyDBContext : DbContext
    {
        public DbSet<Trip> Trips { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=C:\\Users\\amori\\OneDrive\\Documentos\\EstudoNLW\\JourneyDatabase.db");
    }
}

