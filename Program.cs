using Microsoft.EntityFrameworkCore;
using RestASPNETErudio;
using RestASPNETErudio.Model.Context;
using RestASPNETErudio.Services.Implementations;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        



        builder.Services.AddScoped<IPersonService, PersonServiceImplementation>();

        builder.Services.AddDbContext<MySQLContext>(options =>
        {
            var connectionString = builder.Configuration.GetConnectionString("MySQLConnection");
            options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 29)));
        });
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}