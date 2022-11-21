using DictionaryAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace DictionaryAPI.Operations.Delete
{
    public class DeleteWordByNameCommand : IDeleteWordByNameCommand
    {
        private readonly DictionaryDB context;

        public DeleteWordByNameCommand(DictionaryDB context)
        {
            this.context = context;
        }

        public async Task<bool> DeleteByName(string wordName)
        {
            wordName = wordName.Trim();
            var word = await context.Words.FirstOrDefaultAsync(w => w.Name == wordName);
            if (word is not null)
            {
                context.Remove(word);
                return true;
            }
            else
                return false;
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }
    }
}
