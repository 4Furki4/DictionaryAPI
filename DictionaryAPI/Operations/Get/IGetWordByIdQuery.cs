using DictionaryAPI.Context;
using DictionaryAPI.Models.ViewModels;

namespace DictionaryAPI.Operations.Get
{
    public interface IGetWordByIdQuery
    {
        public Task<WordViewModel> GetWordById(long id);
    }
}
