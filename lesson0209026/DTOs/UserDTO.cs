namespace lesson0209026.DTOs;

public class UserDTO
{
    public int Id { get; set; }
    public string Login { get; set; } = string.Empty;
}

public class CreateUserDTO
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class UpdateUserDTO
{
    public int Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public string? Password { get; set; }
}