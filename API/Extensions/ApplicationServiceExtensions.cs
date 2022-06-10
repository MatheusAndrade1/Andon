using System.Text;
using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            
            services.AddScoped<IAndonRepository, AndonRepository>();
            services.AddScoped<INodeListRepository, NodeListRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            
            services.AddScoped<ITokenService, TokenService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                        ValidateIssuer = false, //API server
                        ValidateAudience = false, //front end client
                    };
                });

            services.AddScoped<ClientIpCheckActionFilter>(container =>
            {
                var loggerFactory = container.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger<ClientIpCheckActionFilter>();

                return new ClientIpCheckActionFilter(
                    config["AdminSafeList"], logger);
            });

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}