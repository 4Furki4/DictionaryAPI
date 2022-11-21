using DictionaryAPI.Models.Concretes;
using DictionaryAPI.Models.ViewModels;
using System.Globalization;

namespace DictionaryAPI.Operations
{
    public static class CRUDExtensionMethods
    {
        public static WordViewModel MakeWordAsVM(this Word word)
        {
            WordViewModel viewModel = new WordViewModel();
            viewModel.Name = word.Name;
            foreach (Definition definition in word.Definitions)
            {
                viewModel.Definitions.Add(new()
                {
                    Definition = definition.WordDefinition,
                    DefinitionType = definition.DefinitionType.ToLower(),
                    ExampleSentence = definition.ExampleSentence != null ? definition.ExampleSentence.Trim().makeFirstCapitilized() : null
                });
            }
            return viewModel;
        }

        public static Word ConvertVmToWord(this WordViewModel _word, Word word)
        {
            List<Definition> list = new List<Definition>();
            word.Name = _word.Name.Trim().makeFirstCapitilized();
            if (word.Definitions.Count == 0)
            {
                foreach (DefinitionViewModel definition in _word.Definitions)
                {
                    word.Definitions.Add(
                    new Definition
                    {
                        WordDefinition = definition.Definition.Trim().makeFirstCapitilized(), // definition starts with upper case.
                        DefinitionType = definition.DefinitionType.Trim().ToLower(), // type is lower case 
                        ExampleSentence = definition.ExampleSentence != null ? definition.ExampleSentence.Trim().makeFirstCapitilized() : null // "Example sentence" should be like this
                    }
                    );
                }
            }
            else
            {
                foreach (DefinitionViewModel definition in _word.Definitions)
                {
                    list.Add(new Definition
                    {
                        WordDefinition = definition.Definition.Trim().makeFirstCapitilized(),
                        DefinitionType = definition.DefinitionType.Trim(),
                        ExampleSentence = definition.ExampleSentence != null ? definition.ExampleSentence.Trim().makeFirstCapitilized() : string.Empty
                    });
                }
                word.Definitions = list;
            }
            return word;
        }
        public static string makeFirstCapitilized(this string value)
        {
            return char.ToUpper(value[0], new CultureInfo("tr-TR", false)) + value.Substring(1).ToLower(new CultureInfo("tr-TR", false));
        }
    }
}
