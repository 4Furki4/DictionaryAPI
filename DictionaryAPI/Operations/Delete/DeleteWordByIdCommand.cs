using DictionaryAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace DictionaryAPI.Operations.Delete
{
    public class DeleteWordByIdCommand : IDeleteWordByIdCommand
    {
        private readonly DictionaryDB context;

        public DeleteWordByIdCommand(DictionaryDB context)
        {
            this.context = context;
        }

        public async Task<bool> DeleteById(long id)
        {
            var word = await context.Words.FirstOrDefaultAsync(w => w.Id == id);
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
