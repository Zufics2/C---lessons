using Dapper;
using Npgsql;
using lesson180926.Abstract;
using lesson180926.Model;

namespace lesson180926.Service
{
    public class CityService : ICity
    {
        private readonly string _connectionString;

        public CityService(IConfiguration config) 
        {
            _connectionString = config.GetConnectionString("PostgresConnection") 
                                ?? throw new InvalidOperationException("Строка подключения 'PostgresConnection' не найдена в appsettings.json");
        }

        public IEnumerable<CityDTO> CityGetAll()
        {
            using (var db = new NpgsqlConnection(_connectionString))
            {
                return db.Query<CityDTO>("SELECT id, name, year, population FROM city ORDER BY name");
            }
        }

        public CityDTO CityGetById(int id)
        {
            using (var db = new NpgsqlConnection(_connectionString))
            {
                return db.QueryFirstOrDefault<CityDTO>("SELECT id, name, year, population FROM city WHERE id = @id", new { id })!;
            }
        }

        public string CityAdd(CityDTO city)
        {
            using (var db = new NpgsqlConnection(_connectionString))
            {
                var sql = @"INSERT INTO city (name, year, population) 
                    VALUES (@name, @year, @population) 
                    RETURNING id;";

                int newId = db.QuerySingle<int>(sql, city);

                return $"Город создан с ID: {newId}";
            }
        }

        public string CityEdit(CityDTO city, int id)
        {
            using (var db = new NpgsqlConnection(_connectionString))
            {
                var sql = "UPDATE city SET name = @name, year = @year, population = @population WHERE id = @id";
                db.Execute(sql, new { city.name, city.year, city.population, id });
                return "Город обновлен";
            }
        }

        public string CityDelete(int id)
        {
            using (var db = new NpgsqlConnection(_connectionString))
            {
                int rows = db.Execute("DELETE FROM city WHERE id = @id", new { id }, commandType: System.Data.CommandType.Text);
                return rows.ToString();
            }
        }
    }
}