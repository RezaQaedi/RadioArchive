using Microsoft.EntityFrameworkCore;
using RadioArchive.Core;

namespace RadioArchive.Relational
{

    /// <summary>
    /// The database context for data store 
    /// </summary>
    public class ClientDataStoreDbContext : DbContext
    {
        public DbSet<ShowDataModel> Shows => Set<ShowDataModel>();
        public DbSet<UserNotesDataModel> UserNotes => Set<UserNotesDataModel>();
        public DbSet<UserCreatedListDataModel> UserPlayLists => Set<UserCreatedListDataModel>();

        public ClientDataStoreDbContext(DbContextOptions<ClientDataStoreDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Set up realations 
            modelBuilder.Entity<ShowDataModel>()
                .HasMany(s => s.Notes)
                .WithOne(n => n.Show)
                .HasForeignKey(n => n.CurrentShowId)
                .OnDelete(DeleteBehavior.Cascade);

            // Set up uinqe indexes 
            modelBuilder.Entity<UserCreatedListDataModel>()
                .HasIndex(l => l.Title)
                .IsUnique(true);

            modelBuilder.Entity<ShowDataModel>()
                .HasIndex(s => new { s.Time, s.Date })
                .IsUnique(true);

            modelBuilder.Entity<UserNotesDataModel>()
                .HasIndex(n => n.Date)
                .IsUnique();
        }
    }
}
