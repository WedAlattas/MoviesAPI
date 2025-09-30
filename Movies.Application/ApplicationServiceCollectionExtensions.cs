using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Movies.Application.Database;
using Movies.Application.Options;
using Movies.Application.Repositories;
using Movies.Application.Services;
using System.Text;

namespace Movies.Application
{
    public static class ApplicationServiceCollectionExtensions
    {

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<IMovieRepository, MovieRepository>();
            services.AddSingleton<IIdentityRepository, IdentityRepository>();

            services.AddSingleton<IMovieService, MovieService>();
            services.AddSingleton<IIdentityService, IdentityService>();

            services.AddValidatorsFromAssemblyContaining<IApplicationMarker>(ServiceLifetime.Singleton);

            
            return services;
        }
        public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IDbConnectionFactory>(_ =>
      new NpgsqlConnectionFactory(connectionString));
            services.AddSingleton<DbInitializer>();
            return services;
        }

        public static IServiceCollection AddJwtSettings(this IServiceCollection services, IConfiguration config)
        {

            var JwtSettings = new JwtSettings();
            config.Bind(nameof(JwtSettings), JwtSettings);
            services.AddSingleton(JwtSettings);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(options =>
              {
                  options.SaveToken = true;
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.Secret)),
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      RequireExpirationTime  = false,
                      ValidateLifetime = true
                  };
              });

            return services;
        }
    }
}
