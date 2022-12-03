namespace DictionaryAPI.Models.Concretes
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } 

        public byte[] PasswordSalt { get; set; } 

        public int roleId { get; set; }
    }
}
