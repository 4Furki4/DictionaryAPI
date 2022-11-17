using DictionaryAPI.Models.Concretes;
using Microsoft.EntityFrameworkCore;

namespace DictionaryAPI.Context
{
    public class DictionaryDB : DbContext
    {
        public DictionaryDB(DbContextOptions<DictionaryDB> options) : base(options) { }

        public DbSet<Word> Words { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
        }
    }
}
