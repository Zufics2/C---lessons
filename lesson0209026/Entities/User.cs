using System.ComponentModel.DataAnnotations;

namespace lesson0209026.Entities;

public class User
{
    public int Id { get; set; }
    [MaxLength(900)]
    public string Login { get; set; } = string.Empty;
    [MaxLength(1500)]
    public string Password { get; set; } = string.Empty;
    public List<Order>? UserOrders { get; set; }
    public int RoleId { get; set; } = 1;
}

public enum Role
{
    Admin = 1001, 
    Sales = 101, 
    User = 1 
}