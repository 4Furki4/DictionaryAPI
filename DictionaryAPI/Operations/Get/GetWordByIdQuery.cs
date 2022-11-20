using DictionaryAPI.Context;
using DictionaryAPI.Context.DictionaryRepository;
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
            WordViewModel viewModel = new WordViewModel();
            viewModel.Name = word.Name;
            foreach (Definition definition in word.Definitions)
            {
                viewModel.Definitions.Add(new() {
                    Definition = definition.WordDefinition,
                    DefinitionType = definition.DefinitionType,
                    ExampleSentence = definition.ExampleSentence 
                });
            }
            return viewModel;
        }
    }
}
