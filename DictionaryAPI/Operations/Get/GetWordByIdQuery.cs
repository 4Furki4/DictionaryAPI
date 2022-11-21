using DictionaryAPI.Context;
using DictionaryAPI.Models.Concretes;
using DictionaryAPI.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DictionaryAPI.Operations.Get
{
    public class GetWordByIdQuery : IGetWordByIdQuery
    {
        private DictionaryDB context;

        public GetWordByIdQuery(DictionaryDB context)
        {
            this.context = context;
        }

        public virtual async Task<WordViewModel> GetWordById(long id)
        {
            var word = await context.Words.Include(w => w.Definitions).FirstOrDefaultAsync(word => word.Id == id);
            if (word != null)
                return word.MakeWordAsVM();
            else
                return new WordViewModel();
        }
    }
}
