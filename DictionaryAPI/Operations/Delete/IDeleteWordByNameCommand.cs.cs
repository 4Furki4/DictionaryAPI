namespace DictionaryAPI.Operations.Delete
{
    public interface IDeleteWordByNameCommand
    {
        public Task<bool> DeleteByName(string wordName);
        public Task SaveChanges();
    }
}
