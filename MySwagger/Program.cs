using Asp.Versioning;
using Microsoft.OpenApi;
using System.Reflection;

namespace MySwagger
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My API",
                    Version = "v1",
                    Description = "Описание для первой версии API",
                    Contact = new OpenApiContact
                    {
                        Name = "Иван Иванов",
                        Email = "ivanov@example.com",
                        Url = new Uri("https://t.me/your_username")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });

                options.SwaggerDoc("v2", new OpenApiInfo
                {
                    Title = "My API",
                    Version = "v2",
                    Description = "Описание для второй версии API",
                    Contact = new OpenApiContact
                    {
                        Name = "Иван Иванов",
                        Email = "ivanov@example.com"
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    options.SwaggerEndpoint("/swagger/v2/swagger.json", "My API V2");
                    options.DefaultModelsExpandDepth(-1);
                });
            }

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}