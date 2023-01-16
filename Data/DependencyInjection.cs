using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MainContext>(x =>
            {
                x.UseNpgsql(configuration.GetConnectionString("MainContext"))
                .UseSnakeCaseNamingConvention();
            });

            var idpOptions = configuration.GetSection("Idp");
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(x =>
                    {
                        x.Authority = idpOptions["Authority"];
                        x.Audience = idpOptions["Audience"];
                    });

            return services;
        }
    }
}