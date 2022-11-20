using DictionaryAPI.Models.Abstracts;

namespace DictionaryAPI.Context.DictionaryRepository
{
    public interface IDictionaryRepository
    {
        IQueryable<IWord> Words{ get; } 

        IQueryable<IDefinition> Definitions { get;}
        //void SaveChanges();

        //Task SaveChangesAsync();
    }
}
