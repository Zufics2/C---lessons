using BasicAuth.Models;
using Dapper;
using Npgsql;

namespace BasicAuth.Repositories
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PostgresConnection")
                ?? throw new InvalidOperationException("PostgresConnection is not configured");
        }

        private sealed class Row
        {
            public int UserId { get; set; }
            public string Username { get; set; } = string.Empty;
            public string? RoleName { get; set; }
        }

        private const string BaseSql = @"
            SELECT u.id AS UserId, u.username AS Username, r.name AS RoleName
            FROM users u
            LEFT JOIN user_role ur ON ur.user_id = u.id
            LEFT JOIN role r       ON r.id = ur.role_id";

        /// <summary>All users with all of their roles.</summary>
        public async Task<List<UserWithRoles>> GetAllUsersWithRolesAsync()
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            var rows = await connection.QueryAsync<Row>(BaseSql + " ORDER BY u.id, r.name");
            return Group(rows);
        }

        /// <summary>One user with roles by id, or null if the user doesn't exist.</summary>
        public async Task<UserWithRoles?> GetUserWithRolesByIdAsync(int id)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            var rows = await connection.QueryAsync<Row>(
                BaseSql + " WHERE u.id = @id ORDER BY r.name", new { id });
            return Group(rows).FirstOrDefault();
        }

        /// <summary>Checks credentials in the DB (bcrypt via pgcrypto) and returns the user with roles.</summary>
        public async Task<UserWithRoles?> ValidateCredentialsAsync(string username, string password)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            var rows = await connection.QueryAsync<Row>(
                BaseSql + @" WHERE u.username = @username
                             AND u.password_hash = crypt(@password, u.password_hash)",
                new { username, password });
            return Group(rows).FirstOrDefault();
        }

        private static List<UserWithRoles> Group(IEnumerable<Row> rows) =>
            rows.GroupBy(r => new { r.UserId, r.Username })
                .Select(g => new UserWithRoles
                {
                    Id = g.Key.UserId,
                    Username = g.Key.Username,
                    Roles = g.Where(r => r.RoleName != null).Select(r => r.RoleName!).ToList()
                })
                .ToList();
    }
}