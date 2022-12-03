using DictionaryAPI.Models.Concretes;
using Microsoft.EntityFrameworkCore;

namespace DictionaryAPI.Context
{
    public class DictionaryDB : DbContext
    {
        public DictionaryDB(DbContextOptions<DictionaryDB> options) : base(options) { }

        public virtual DbSet<Word> Words => Set<Word>();

        public virtual DbSet<Definition> Definitions => Set<Definition>();

        public DbSet<User> Users => Set<User>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Word>().HasMany(w => w.Definitions).WithOne(d => d.Word); // One to many relation between Word and Definition
        }
    }
}
