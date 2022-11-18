using DictionaryAPI.Models.Concretes;

namespace DictionaryAPI.Models.Abstracts
{
    public interface IDefinition
    {
        public long Id { get; set; }
        public Word Word { get; set; }
        public long WordId { get; set; }
        public string WordDefinition { get; set; }
        public string DefinitionType { get; set; }
        public string ExampleSentence { get; set; }
    }
}