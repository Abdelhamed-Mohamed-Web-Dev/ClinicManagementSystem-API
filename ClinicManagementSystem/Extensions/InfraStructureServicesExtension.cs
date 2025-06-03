using Microsoft.Extensions.Options;
using Persistence.Identity;
using Service.Abstraction;
using Service;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace ClinicManagementSystem.Extensions
{
    public static class InfraStructureServicesExtension
    {
        public static IServiceCollection AddInfraStructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MainContext>(o => o.UseSqlServer(configuration.GetConnectionString("DefaultSqlConnection")));
            services.AddDbContext<StoreIdentityContext>(o => o.UseSqlServer(configuration.GetConnectionString("IdentitySqlConnection")));
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.ConfigreIdentityService();
            return services;
        }

      public  static  IServiceCollection ConfigreIdentityService(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
                options.User.RequireUniqueEmail = true;

            }
            ).AddEntityFrameworkStores<StoreIdentityContext>();
            return services;
        }
    }

}
