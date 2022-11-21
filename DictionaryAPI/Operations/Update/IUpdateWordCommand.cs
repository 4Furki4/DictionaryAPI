using DictionaryAPI.Models.Concretes;
using DictionaryAPI.Models.ViewModels;

namespace DictionaryAPI.Operations.Update
{
    public interface IUpdateWordCommand
    {
        public Task<bool> Update(WordViewModel _word, string wordName);
    }
}
