using System.Text.Json.Serialization;

using Service;
using Service.Abstraction;

namespace ClinicManagementSystem
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddDbContext<MainContext>(o=>o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSqlConnection")));
			builder.Services.AddScoped<IDbInitializer,DbInitializer>();
			builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
			builder.Services.AddScoped<IServiceManager, ServiceManager>();
			builder.Services.AddAutoMapper(typeof(AssemblyReference).Assembly);
            //////			builder.Services.AddControllers().AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
            builder.Services.AddControllers()
                .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly)
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            //builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            	builder.Services.AddSwaggerGen();
       //     builder.Services.AddScalar();

            var app = builder.Build();

			await DataSeeding(app);

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
			app.UseSwagger();
			app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
		// Data Seeding Method
		static async Task DataSeeding(WebApplication app)
		{
			// Create Scope 
			using var scope = app.Services.CreateScope();
			// Inject
			var initDb = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
			// Call Initializer
			await initDb.InitializeAsync();
		}
	}
}
