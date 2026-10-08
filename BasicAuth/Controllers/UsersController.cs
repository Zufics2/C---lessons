using BasicAuth.Models;
using BasicAuth.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasicAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly UserRepository _users;

        public UsersController(UserRepository users) => _users = users;

        // GET api/users  -> all users with all their roles
        [HttpGet]
        public async Task<ActionResult<List<UserWithRoles>>> GetAll()
            => Ok(await _users.GetAllUsersWithRolesAsync());

        // GET api/users/3/roles -> roles of a single user
        [HttpGet("{id:int}/roles")]
        public async Task<ActionResult<UserWithRoles>> GetRoles(int id)
        {
            var user = await _users.GetUserWithRolesByIdAsync(id);
            return user is null ? NotFound($"User {id} not found") : Ok(user);
        }
    }
}