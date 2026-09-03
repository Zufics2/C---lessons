using lesson0209026.DTOs;
using lesson0209026.Entities;
using lesson0209026.Services;
using Microsoft.AspNetCore.Mvc;

namespace lesson0209026.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<User>>> GetUsers()
    {
        var users = await userService.GetUsers();
        return Ok(users);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await userService.GetUser(id);

        if (user == null)
        {
            return NotFound($"Пользователь с Id {id} не найден.");
        }

        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> AddUser([FromBody] CreateUserDTO dto)
    {
        var createdUser = await userService.AddUserAsync(dto);
        return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
    }

    [HttpPut]
    public async Task<ActionResult<User>> UpdateUser([FromBody] UpdateUserDTO dto)
    {
        var updatedUser = await userService.UpdateUserAsync(dto);

        if (updatedUser == null)
        {
            return NotFound($"Пользователь с Id {dto.Id} не найден.");
        }

        return Ok(updatedUser);
    }
}