using DictionaryAPI.Context.DictionaryRepository;
using DictionaryAPI.Models.Abstracts;
using DictionaryAPI.Models.Concretes;
using Microsoft.EntityFrameworkCore;

namespace DictionaryAPI.Context.DictionaryStore
{
    public class DictionaryRepository : DbContext, IDictionaryRepository
    {
        private DictionaryDB context;

        public DictionaryRepository(DictionaryDB context)
        {
            this.context = context;
        }

        public IQueryable<IWord> Words => context.Words;
        public IQueryable<IDefinition> Definitions => context.Definitions;

        //public void SaveChanges()
        //{
        //    context.SaveChanges();
        //}

        //public async Task SaveChangesAsync()
        //{
        //    await context.SaveChangesAsync();
        //}
    }
}
