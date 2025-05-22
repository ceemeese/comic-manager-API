using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Models;

namespace Data
{
    
    public class DataContext : DbContext
    {   

    public DataContext(DbContextOptions<DataContext> options) : base(options) {}

    public DbSet<Comic> Comics { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<User> Users{ get; set; }
    public DbSet<ComicGenre> ComicsGenres{ get; set; }
    public DbSet<UserComic> UsersComics{ get; set; }
    
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        //tabla intermedia muchos a muchos ComicGenre
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


        //tabla intermedia muchos a muchos UserComic
        modelBuilder.Entity<UserComic>()
            .HasKey(uc => new { uc.UserId, uc.ComicId });


        modelBuilder.Entity<UserComic>()
            .HasOne(uc => uc.User)
            .WithMany(uc => uc.UserComics)
            .HasForeignKey(uc => uc.UserId);


        modelBuilder.Entity<UserComic>()
            .HasOne(uc => uc.Comic)
            .WithMany(uc => uc.UserComics)
            .HasForeignKey(uc => uc.ComicId);
        

        modelBuilder.Entity<User>()
            .HasData(
                new User
                {
                    Id = 1,
                    Name = "Admin", 
                    Mail = "admin@admin.com", 
                    Password = "admin", 
                    Telephone = "666666666",
                }
             );
        }
    }
}
