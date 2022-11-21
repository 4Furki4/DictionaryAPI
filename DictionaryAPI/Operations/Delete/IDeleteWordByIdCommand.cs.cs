namespace DictionaryAPI.Operations.Delete
{
    public interface IDeleteWordByIdCommand
    { 
        public Task<bool> DeleteById(long id);
        public Task SaveChanges();
    }
}
