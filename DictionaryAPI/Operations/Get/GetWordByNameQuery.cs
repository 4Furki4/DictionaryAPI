using DictionaryAPI.Context;
using DictionaryAPI.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DictionaryAPI.Operations.Get
{
    public class GetWordByNameQuery : IGetWordByNameQuery
    {
        private DictionaryDB context;

        public GetWordByNameQuery(DictionaryDB context)
        {
            this.context = context;
        }

        public async Task<WordViewModel> GetWordByName(string wordName)
        {
            wordName= wordName.Trim();
            var word = await context.Words.Include(w => w.Definitions).FirstOrDefaultAsync(w => w.Name == wordName);
            return word.MakeWordAsVM();        
        }
    }
}
