using DataLayer;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Models.Settings;
using ServiceLayer.Services;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            builder.Services.Configure<SuperuserSettings>(builder.Configuration.GetSection("SuperuserSettings"));

            builder.Services.AddControllers();
            builder.Services.AddAppSwagger();
            builder.Services.AddAppServices();
            builder.Services.AddAppIdentity();
            builder.Services.ConfigureApp(builder.Configuration); 
            builder.Services.AddAppDbContext(builder.Configuration);
            builder.Services.AddAppAuthentication(builder.Configuration);
            builder.Services.AddHostedService<DatabaseInitService>();

            builder.Services.AddDbContext<AppDbContext>(options
               => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
