using DictionaryAPI.Models.Concretes;
using Microsoft.EntityFrameworkCore;

namespace DictionaryAPI.Context
{
    public class DictionaryDB : DbContext
    {
        public DictionaryDB(DbContextOptions<DictionaryDB> options) : base(options) { }

        public DbSet<Word> Words { get; set; }

        public DbSet<Definition> Definitions {get; set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Word>().HasMany(w => w.Definitions).WithOne(d => d.Word); // One to many relation between Word and Definition
        }
    }
}
