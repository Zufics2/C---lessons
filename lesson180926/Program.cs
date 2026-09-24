using lesson180926.Abstract;
using lesson180926.Model;
using lesson180926.Service;

namespace lesson180926
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // Mapper
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
                cfg.AddProfile<MappingProfile2>();
                cfg.AddProfile<MappingProfile3>();
            });

            builder.Services.AddScoped<IStudent, WorkService>();
            builder.Services.AddScoped<ICity, CityService>();

            builder.Services.AddControllers();
            
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
