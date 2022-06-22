namespace API.DTOs
{
    public class NodeListDto
    {
        public int id { get; set; }
        public string entityId { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string hierarchyDefinitionId { get; set; }
        public string hierarchyId { get; set; }
        public string parentEntityId { get; set; }
        public string path { get; set; }
    }
}