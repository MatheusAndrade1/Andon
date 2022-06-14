namespace API.DTOs
{
    public class AndonUpdateDto
    {
        public string name { get; set; }
        public string hierarchyDefinitionId { get; set; }
        public string hierarchyId { get; set; }
        public string parentEntityId { get; set; }
        public string path { get; set; }
    }
}