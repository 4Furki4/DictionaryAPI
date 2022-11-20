using DictionaryAPI.Models.Concretes;
using DictionaryAPI.Models.ViewModels;

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
                    DefinitionType = definition.DefinitionType,
                    ExampleSentence = definition.ExampleSentence
                });
            }
            return viewModel;
        }

        public static Word ConvertVmToWord(this WordViewModel _word, Word word)
        {
            List<Definition> list = new List<Definition>();
            word.Name = _word.Name;
            if (word.Definitions.Count == 0)
            {
                foreach (DefinitionViewModel definition in _word.Definitions)
                {
                    word.Definitions.Add(
                    new Definition
                    {
                        WordDefinition = definition.Definition,
                        DefinitionType = definition.DefinitionType,
                        ExampleSentence = definition.ExampleSentence
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
                        WordDefinition = definition.Definition,
                        DefinitionType = definition.DefinitionType,
                        ExampleSentence = definition.ExampleSentence
                    });
                }
                word.Definitions = list;
            }
            return word;
        }
    }
}
