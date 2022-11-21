using DictionaryAPI.Context;
using DictionaryAPI.Models.Concretes;
using DictionaryAPI.Models.ViewModels;


namespace DictionaryAPI.Operations.Create
{
    public class CreateWordCommand : ICreateWordCommand
    {
        private readonly DictionaryDB context;

        public CreateWordCommand(DictionaryDB context)
        {
            this.context = context;
        }

        public Word CreateNewWord(WordViewModel _word)
        {
            Word word = _word.ConvertVmToWord(new Word());
            return word;
        }

        public async Task SaveNewWord(Word word)
        {
            await context.Words.AddAsync(word);
            await context.SaveChangesAsync();
        }

    }
}
