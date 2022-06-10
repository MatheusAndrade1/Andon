using Microsoft.EntityFrameworkCore;

namespace API.Entities
{
    [Index(nameof(Username), IsUnique = true)]
    public class AppUser
    {
        public int id {get; set;}
        public string Username {get; set;}
        public byte[] PasswordHash {get; set;}
        public byte[] PasswordSalt {get; set;}
    }
}