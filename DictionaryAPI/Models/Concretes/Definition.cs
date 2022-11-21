using DictionaryAPI.Models.Abstracts;
using System.ComponentModel.DataAnnotations;

namespace DictionaryAPI.Models.Concretes
{
    public class Definition : IDefinition
    {
        public long Id { get; set; }
        public long WordId { get; set; }
        public virtual Word Word { get; set; } = new Word();
        public string WordDefinition { get; set; } = string.Empty;
        public string DefinitionType { get; set; } = string.Empty;
        public string? ExampleSentence { get; set; } = string.Empty;
    }
}
