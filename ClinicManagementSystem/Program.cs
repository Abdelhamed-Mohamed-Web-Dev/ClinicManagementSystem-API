

using ClinicManagementSystem.MiddleWares;

namespace ClinicManagementSystem
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddCoreServices(builder.Configuration);
			builder.Services.AddInfraStructureServices(builder.Configuration);
			builder.Services.AddPresentationService();
         


		//	builder.Services.AddAutoMapper(typeof(AssemblyReference).Assembly);
           
            var app = builder.Build();

			app.UseMiddleware<ExceptionHandlerMiddleware>();

			await DataSeeding(app);

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
			app.UseSwagger();
			app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.UseAuthentication();
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
			// Initialization Data Base
			await initDb.InitializeAsync();
            // Initialization Security DataBase
            await initDb.InitializeIdentityAsync();
		}
	   

	}
}
//////			builder.Services.AddControllers().AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
