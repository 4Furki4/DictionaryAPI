using DictionaryAPI.Models.Concretes;
using DictionaryAPI.Models;
using static DictionaryAPI.Models.Validations;
using DictionaryAPI.Models.ViewModels;
using FluentValidation;

namespace DictionaryAPI.Operations.Create
{
    public class CreateWordValidation
    {
        public async Task Validate(WordViewModel _word)
        {
            WordViewModelValidation validations = new WordViewModelValidation();
            DefinitionViewModelValidation validationDefinition = new DefinitionViewModelValidation();

            await validations.ValidateAndThrowAsync(_word);
            foreach (var definition in _word.Definitions)
                await validationDefinition.ValidateAndThrowAsync(definition);
        }
    }
}

