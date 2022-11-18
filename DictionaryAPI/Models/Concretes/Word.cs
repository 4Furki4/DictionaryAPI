using DictionaryAPI.Models.Abstracts;

namespace DictionaryAPI.Models.Concretes
{
    public class Word : IWord
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Definition> Definitions { get; set;} = new List<Definition>();
    }
}
