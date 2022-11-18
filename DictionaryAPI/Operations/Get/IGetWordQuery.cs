using DictionaryAPI.Context;
using DictionaryAPI.Models.ViewModels;

namespace DictionaryAPI.Operations.Get
{
    public interface IGetWordQuery
    {
        public Task<WordViewModel> GetWordById(long id);
    }
}
