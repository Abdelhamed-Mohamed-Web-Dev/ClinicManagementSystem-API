
namespace ClinicManagementSystem.Extensions
{
    public static class CoreServicesExtension
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(AssemblyReference).Assembly);
            services.AddScoped<IServiceManager, ServiceManager>();

            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
            return services;
        }
    }
}
