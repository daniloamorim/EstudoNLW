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

    /* isso e uso para nao ter um link ditero a tabela de Activities 
     dessa forma estou acessando minha tabela Activities atravez da mina tabela Trips*/
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Activity>().ToTable("Activities");
    }
}

