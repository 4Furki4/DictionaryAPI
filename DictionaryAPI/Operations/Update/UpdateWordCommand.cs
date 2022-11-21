using DictionaryAPI.Context;
using DictionaryAPI.Models.Concretes;
using DictionaryAPI.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DictionaryAPI.Operations.Update
{
    public class UpdateWordCommand : IUpdateWordCommand
    {
        private DictionaryDB context;

        public UpdateWordCommand(DictionaryDB context)
        {
            this.context = context;
        }

        public async Task<bool> Update(WordViewModel _word, string wordName)
        {
            wordName = wordName.Trim();
            var word = await context.Words.Include(w => w.Definitions).FirstOrDefaultAsync(w => w.Name == wordName);
            if (word != null)
            {
                word = _word.ConvertVmToWord(word);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
            
        }
    }
}
