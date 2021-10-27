using System;
using Infrastructure.Data.DbContext;
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
                        //.SetIsOriginAllowed(origin => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        //.WithExposedHeaders("X-Pagination")
                        .AllowCredentials());
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


        // public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        //     services.AddScoped<IRepositoryManager, RepositoryManager>();
        
    }
}