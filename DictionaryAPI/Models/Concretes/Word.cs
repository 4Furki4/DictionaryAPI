using DictionaryAPI.Models.Abstracts;
using System.ComponentModel.DataAnnotations;

namespace DictionaryAPI.Models.Concretes
{
    public class Word : IWord
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual List<Definition> Definitions { get; set;} = new List<Definition>();
    }
}
