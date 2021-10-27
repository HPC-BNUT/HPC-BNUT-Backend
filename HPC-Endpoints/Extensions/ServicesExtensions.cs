using System;
using ApplicationService.CommandHandlers;
using Domain._Shared.Repositories;
using Infrastructure.Data.DbContext;
using Infrastructure.Data.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HPC_Endpoints.Extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
        }

        public static void ConfigureSqlContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            var cs1 = Environment.GetEnvironmentVariable("PgSqlConnection");
            var cs2 = configuration.GetConnectionString("PgSqlConnection");
            services.AddDbContext<PgSqlDbContext>(opts =>
                opts.UseNpgsql(cs2, b =>
                    b.MigrationsAssembly("Infrastructure")));
        }

        public static void ConfigureMediatR(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));
        }
        
        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureApplicationDomain(this IServiceCollection services) =>
            services.AddScoped<RegisterUserHandler>();

    }
}