using DictionaryAPI.Models.Concretes;
using DictionaryAPI.Models.ViewModels;

namespace DictionaryAPI.Operations.Create
{
    public interface ICreateWordCommand
    {
        public Word CreateNewWord(WordViewModel _word);

        public Task SaveNewWord(Word word);
    }
}
