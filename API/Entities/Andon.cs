using Microsoft.EntityFrameworkCore;

namespace API.Entities
{
    [Index(nameof(type), IsUnique = true)]
    public class Andon
    {
        public int id { get; set; }
        public string type { get; set; }
        public int warnCount { get; set; }
        public int alarmCount { get; set; }
        public string entityId { get; set; }
    }
}