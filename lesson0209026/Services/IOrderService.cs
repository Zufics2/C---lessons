using lesson0209026.AppDataContext;
using lesson0209026.DTOs;
using lesson0209026.Entities;
using Microsoft.EntityFrameworkCore;

namespace lesson0209026.Services;

public interface IOrderService
{
    Task<List<Order>> GetOrders();
    Task<Order?> GetOrder(int id);
    Task<Order> AddOrderAsync(CreateOrderDTO dto);
    Task<Order?> UpdateOrderAsync(UpdateOrderDTO dto);
}

public class OrderService(AppDbContext dbContext) : IOrderService
{
    public async Task<List<Order>> GetOrders()
    {
        return await dbContext
            .Orders
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Order?> GetOrder(int id)
    {
        return await dbContext
            .Orders
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Order> AddOrderAsync(CreateOrderDTO dto)
    {
        var order = new Order
        {
            Title = dto.Title,
            CreateTime = DateTime.UtcNow
        };

        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync();

        return order;
    }

    public async Task<Order?> UpdateOrderAsync(UpdateOrderDTO dto)
    {
        var order = await dbContext
            .Orders
            .FirstOrDefaultAsync(x => x.Id == dto.Id);

        if (order == null)
        {
            return null;
        }

        order.Title = dto.Title;

        await dbContext.SaveChangesAsync();

        return order;
    }
}