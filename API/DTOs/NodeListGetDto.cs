namespace API.DTOs
{
    public class NodeListGetDto
    {
        public string entityId { get; set; }
        public string name { get; set; }
        public Dictionary<string, string>[] paths { get; set; }
    }
}