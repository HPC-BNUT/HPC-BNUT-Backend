using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ApplicationService._Shared.Services;
using ApplicationService.CommandHandlers;
using ApplicationService.QueryHandlers;
using Domain._Shared.Repositories;
using Framework.Enums;
using Framework.Exceptions;
using Infrastructure;
using Infrastructure.Data.DbContext;
using Infrastructure.Data.Repositories;
using Infrastructure.Mapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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
            var cs2 = configuration.GetConnectionString("Docker-PgSqlConnection");
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

        public static void ConfigureApplicationDomain(this IServiceCollection services)
        {
            services.AddScoped<RegisterUserHandler>();
            services.AddScoped<GetUserByEmailHandler>();
            services.AddScoped<LoginUserHandler>();
            services.AddScoped<RefreshUserHandler>();
        }

        public static void ConfigureMapper(this IServiceCollection services)
        {
            services.AddScoped<ICommandMapper, CommandMapper>();
            services.AddScoped<IQueryMapper, QueryMapper>();
            services.AddScoped<IDtoMapper, DtoMapper>();
        }
          
        public static void AddJwt(this IServiceCollection services)
        {
            services.AddScoped<IJwtTokenCreator, JwtTokenCreator>();
        }
        
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    var secretKey = Encoding.UTF8.GetBytes(configuration["AuthenticationOptions:SecretKey"]);

                    var validationParameters = new TokenValidationParameters
                    {
                        ClockSkew = TimeSpan.Zero, // default: 5 min
                        RequireSignedTokens = true,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                        
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        
                        ValidateAudience = true, //default : false
                        ValidAudience = configuration["AuthenticationOptions:Audience"],
                        
                        ValidateIssuer = true, //default : false
                        ValidIssuer = configuration["AuthenticationOptions:Issuer"]
                    };

                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = validationParameters;
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context => throw new AppException(ApiResultStatusCode.UnAuthorized, "fucked",
                            HttpStatusCode.Unauthorized),
                        OnChallenge = context => throw new AppException(ApiResultStatusCode.UnAuthorized, "Access Token is not valid.",
                            HttpStatusCode.Unauthorized),
                        OnForbidden = context => throw new AppException(ApiResultStatusCode.UnAuthorized, "Permission denied.",
                            HttpStatusCode.Forbidden),
                        OnMessageReceived = context => Task.CompletedTask,
                        OnTokenValidated = context => Task.CompletedTask,
                    };
                });
        }
        
        public static void AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "HPC-Endpoints", Version = "v1"});
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Place for submit accessToken",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name = "Bearer"
                        },
                        new List<string>()
                    }
                });
            });
        }

    }
}