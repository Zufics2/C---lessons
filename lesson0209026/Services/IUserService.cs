using lesson0209026.AppDataContext;
using lesson0209026.DTOs;
using lesson0209026.Entities;
using Microsoft.EntityFrameworkCore;

namespace lesson0209026.Services;

public interface IUserService
{
    Task<List<User>> GetUsers();
    Task<User> AddUserAsync(CreateUserDTO dto);
    Task<User> UpdateUserAsync(UpdateUserDTO dto);
    Task<User> GetUser(int id);
}

public class UserService(AppDbContext dbContext) : IUserService
{
    public async Task<List<User>> GetUsers()
    {
        return await dbContext
            .users
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<User?> GetUser(int id)
    {
        return await dbContext
            .users
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User> AddUserAsync(CreateUserDTO dto)
    {
        var user = new User
        {
            Login = dto.Login,
            Password = dto.Password
        };

        dbContext.users.Add(user);
        await dbContext.SaveChangesAsync();

        return user;
    }

    public async Task<User> UpdateUserAsync(UpdateUserDTO dto)
    {
        var user = await dbContext
            .users
            .FirstOrDefaultAsync(x => x.Id == dto.Id);

        if (user == null)
        {
            return null;
        }

        user.Login = dto.Login;

        if (!string.IsNullOrEmpty(dto.Password))
        {
            user.Password = dto.Password;
        }

        await dbContext.SaveChangesAsync();

        return user;
    }
}