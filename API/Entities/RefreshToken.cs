namespace API.Entities
{
    public class RefreshToken
    {
        public int id {get; set;}
        public string Token {get; set;}
        public int UserId {get; set;}
    }
}