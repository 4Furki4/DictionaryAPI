using DictionaryAPI.Models.Concretes;

namespace DictionaryAPI.Models.Abstracts
{
    public interface IWord
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public List<Definition> Definitions {get; set;}
    }
}
