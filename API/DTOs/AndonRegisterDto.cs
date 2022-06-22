namespace API.DTOs
{
    public class AndonRegisterDto
    {
        public string type { get; set; }
        public int warnCount { get; set; }
        public int alarmCount { get; set; }
        public string entityId { get; set; }
    }
}