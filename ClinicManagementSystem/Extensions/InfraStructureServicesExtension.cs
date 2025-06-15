
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
            services.ConfigureIdentityService();
            services.ConfigureJwt(configuration);
            return services;
        }

      public  static  IServiceCollection ConfigureIdentityService(this IServiceCollection services)
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
        public static IServiceCollection ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,


                        ValidAudience = jwtOptions.Audience,
                        ValidIssuer = jwtOptions.Issure,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))

                    };

                });

            services.AddAuthorization();
            return services;
        }

    }

}
