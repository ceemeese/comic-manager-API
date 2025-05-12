using Microsoft.EntityFrameworkCore;
using Models;

namespace ComicManagerAPI.Data
{
    
    public class DataContext : DbContext
    {   

    public DataContext(DbContextOptions<DataContext> options)
        : base(options) {}

    public DbSet<Comic> Comics { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<User> Users{ get; set; }
    
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        
        modelBuilder.Entity<ComicGenre>()
            .HasKey(cg => new { cg.ComicId, cg.GenreId });


            modelBuilder.Entity<ComicGenre>()
            .HasOne(cg => cg.Comic)
            .WithMany(cg => cg.ComicGenres)
            .HasForeignKey(cg => cg.ComicId);


            modelBuilder.Entity<ComicGenre>()
            .HasOne(cg => cg.Genre)
            .WithMany(cg => cg.ComicGenres)
            .HasForeignKey(cg => cg.GenreId);
            
        }

    }

}