using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DictionaryAPI.Models.Concretes
{
    public class RefreshToken
    {
        [Key]
        public long Id { get; set; }
        public string Token { get; set; } = string.Empty;

        public DateTime TokenCreated { get; set; } = DateTime.Now;

        public DateTime Expires { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }
    }
}
