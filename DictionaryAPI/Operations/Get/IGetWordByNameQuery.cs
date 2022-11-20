using DictionaryAPI.Models.ViewModels;

namespace DictionaryAPI.Operations.Get
{
    public interface IGetWordByNameQuery
    {
        public Task<WordViewModel> GetWordByName(string wordName);
    }
}
