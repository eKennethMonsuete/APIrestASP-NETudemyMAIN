using Microsoft.EntityFrameworkCore;
using RestASPNETErudio;
using RestASPNETErudio.Model.Context;
using RestASPNETErudio.Business.Implementations;
using APIrestASP_NETudemy.Business;


using Serilog;
using EvolveDb;

using APIrestASP_NETudemy.Business.Implementations;


using APIrestASP_NETudemy.Repository.Generic;
using Microsoft.Net.Http.Headers;
using APIrestASP_NETudemy.Hypermedia.Filters;
using APIrestASP_NETudemy.Hypermedia.Enricher;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Rewrite;
using APIrestASP_NETudemy.Services;
using APIrestASP_NETudemy.Services.Implementation;
using APIrestASP_NETudemy.Repository;
using APIrestASP_NETudemy.Configurations;
using Microsoft.Extensions.Options;
using System.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var appName = "Rest aPI RESTful with ASP.NET 9";
        var appVersion = "v1";
        var appDescription = $" qualquer coisa '{appName}'";

        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(
                appVersion,
                new OpenApiInfo
                {
                    Title = appName,
                    Version = appVersion,
                    Description = appDescription,
                    Contact = new OpenApiContact
                    {
                        Name = "Algum nome",
                        Url = new Uri("https://github.com")
                    }


                });
        });


        builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
        builder.Services.AddScoped<IBookBusiness, BookServiceImplementation>();
        builder.Services.AddScoped<ILoginBusiness, LoginBusinessImplementation>();

        builder.Services.AddTransient<ITokenService, TokenService >();

        builder.Services.AddScoped<IUserRepository, UserRepository>();




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

        var tokenConfigurations = new TokenConfiguration();

        new ConfigureFromConfigurationOptions<TokenConfiguration>(
            builder.Configuration.GetSection("TokenConfiguration")).Configure(tokenConfigurations);

        builder.Services.AddSingleton(tokenConfigurations);

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = tokenConfigurations.Issuer,
                ValidAudience = tokenConfigurations.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.Secret))
            };
        });

        
        builder.Services.AddAuthorization(auth =>
        {
            auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser().Build());
        });





        builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
        {
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        }));

        builder.Services.AddMvc(options =>
        {
            options.RespectBrowserAcceptHeader = true;
            options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
            options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
        }).AddXmlSerializerFormatters();


        var filterOptions = new HyperMediaFilterOptions();
        filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
        filterOptions.ContentResponseEnricherList.Add(new BookEnricher());

        builder.Services.AddSingleton(filterOptions);


        builder.Services.AddApiVersioning();



        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseCors();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json",
                $"{appName} - {appVersion}");

        });

        var option = new RewriteOptions();
        option.AddRedirect("^$", "swagger");
        app.UseRewriter(option);


        app.UseAuthorization();

        app.MapControllers();

        app.MapControllerRoute("DefaultApi", "{controller=values}/v{version=apiVersion}/{id?}");

       

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

