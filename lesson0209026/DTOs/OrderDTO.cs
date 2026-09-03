namespace lesson0209026.DTOs;

public class OrderDTO
{
    public int Id { get; set; }
    public DateTime CreateTime { get; set; }
    public string Title { get; set; } = string.Empty;
}

public class CreateOrderDTO
{
    public string Title { get; set; } = string.Empty;
}

public class UpdateOrderDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
}