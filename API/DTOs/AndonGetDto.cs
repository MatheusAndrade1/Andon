namespace API.DTOs
{
    public class AndonGetDto
    {
        public string entityId { get; set; }
        public string name { get; set; }
        public Dictionary<string, string> paths { get; set; }
    }
}