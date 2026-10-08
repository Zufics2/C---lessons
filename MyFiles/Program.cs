using System.Data;
using Microsoft.OpenApi;
using Npgsql;

namespace MyFiles
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddScoped<IDbConnection>(_ =>
                new NpgsqlConnection(
                    builder.Configuration.GetConnectionString("PostgresConnection")
                ));
            
            builder.Services.AddControllers();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "MyFiles API", 
                    Version = "v1" 
                });
            });

            var app = builder.Build();
            

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyFiles API v1");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}