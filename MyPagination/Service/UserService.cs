using Dapper;
using Npgsql;
using MyPagination.Abstract;
using MyPagination.Model;
using System.Data;

namespace MyPagination.Service
{
    public class UserService : IUser
    {
        private readonly IConfiguration _config;

        public UserService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<PagedResult<User>> GetUsersAsync(int page, int pageSize)
        {
            using IDbConnection db = new NpgsqlConnection(_config.GetConnectionString("PostgresConnection"));

            var parameters = new DynamicParameters();
            parameters.Add("p_pagenumber", page);
            parameters.Add("p_pagesize", pageSize);

            const string sql = "SELECT * FROM get_user_page(@p_pagenumber, @p_pagesize);";
            
            var rawData = (await db.QueryAsync<dynamic>(sql, parameters)).ToList();
            
            int totalCount = rawData.Count > 0 ? (int)rawData.First().totalcount : 0;
            
            var users = rawData.Select(x => new User
            {
                id = x.id,
                created = x.created,
                last_name = x.last_name,
                first_name = x.first_name,
                date_birth = x.date_birth,
                email = x.email
            }).ToList();

            return new PagedResult<User>
            {
                Items = users,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}