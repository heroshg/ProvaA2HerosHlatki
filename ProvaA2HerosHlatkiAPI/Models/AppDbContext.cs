using Microsoft.EntityFrameworkCore;

namespace ProvaA2HerosHlatkiAPI.Models
{
    public class AppDbContext : DbContext
    {

        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Folha> Folhas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Funcionario>()
                .HasMany(f => f.Folhas)
                .WithOne(f => f.Funcionario)
                .HasForeignKey(f => f.FuncionarioId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=app.db");
        }
    }
}
