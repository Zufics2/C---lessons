namespace BasicAuth.Models
{
    public class UserWithRoles
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new();
    }
}