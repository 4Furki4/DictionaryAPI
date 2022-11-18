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
            Word word = new Word();
            word.Name = _word.Name;
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
            return word;
        }

        public async Task SaveNewWord(Word word)
        {
            await context.Words.AddAsync(word);
            await context.SaveChangesAsync();
        }
    }
}
