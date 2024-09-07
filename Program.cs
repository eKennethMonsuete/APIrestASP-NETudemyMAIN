using Microsoft.EntityFrameworkCore;
using RestASPNETErudio;
using RestASPNETErudio.Model.Context;
using RestASPNETErudio.Business.Implementations;
using APIrestASP_NETudemy.Business;
using RestASPNETErudio.Repository.Implementations;

using Serilog;
using EvolveDb;

using APIrestASP_NETudemy.Business.Implementations;


using APIrestASP_NETudemy.Repository.Generic;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();


        builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
        builder.Services.AddScoped<IPersonRepository, PersonRepositoryImplementation>();

        builder.Services.AddScoped<IBookBusiness, BookServiceImplementation>();
        

        builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

        builder.Services.AddDbContext<MySQLContext>(options =>
        {
            var connectionString = builder.Configuration.GetConnectionString("MySQLConnection");
            options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 29)));

            if (builder.Environment.IsDevelopment())
            {
                MigrateDatabase(connectionString);
            }

        });





        builder.Services.AddApiVersioning();

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();



        void MigrateDatabase(string connectionString)
        {
            try
            {
                // Corrigido: instancia um objeto da classe MySqlConnection
                using (var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
                {
                    // Abre a conexão
                    evolveConnection.Open();

                    // Instancia Evolve e define suas propriedades
                    var evolve = new Evolve(evolveConnection, msg => Log.Information(msg))
                    {
                        Locations = new List<string>() { "db/migrations", "db/dataset" },
                        IsEraseDisabled = true,
                    };

                    // Executa a migração
                    evolve.Migrate();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Database migration failed", ex);
                throw;
            }

        }
    }
}

