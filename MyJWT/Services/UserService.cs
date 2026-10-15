using Dapper;
using Npgsql;
using MyJWT.Abstract;
using MyJWT.Models;

namespace MyJWT.Services
{
    public class UserService : IUser
    {
        private readonly string _connectionString;

        public UserService(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection") 
                                ?? throw new InvalidOperationException("Connection string not configured");
        }

        public UserEntity? Authenticate(string login, string password)
        {
            using (var db = new NpgsqlConnection(_connectionString))
            {
                var sql = @"SELECT 
                                id AS Id, 
                                login AS Login, 
                                password AS Password, 
                                department AS Department, 
                                roles AS Roles 
                            FROM users 
                            WHERE login = @login AND password = @password";

                return db.QueryFirstOrDefault<UserEntity>(sql, new { login, password });
            }
        }
    }
}