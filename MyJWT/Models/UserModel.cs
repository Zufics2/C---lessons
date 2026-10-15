namespace MyJWT.Models
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Department { get; set; }
        public string Roles { get; set; } = null!;
    }
}