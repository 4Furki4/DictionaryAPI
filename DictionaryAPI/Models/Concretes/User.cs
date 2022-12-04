using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DictionaryAPI.Models.Concretes
{
    public class User
    {
        [Key]
        public long Id { get; set; }
        public int roleId { get; set; }
        public string Name { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } 

        public byte[] PasswordSalt { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}
