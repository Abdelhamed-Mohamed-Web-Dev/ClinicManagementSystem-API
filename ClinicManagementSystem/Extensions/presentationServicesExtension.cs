using ClinicManagementSystem.Factories;
using System.Text.Json.Serialization;

namespace ClinicManagementSystem.Extensions
{
    public static class presentationServicesExtension
    {
        public static IServiceCollection AddPresentationService(this IServiceCollection services)
        {
            services.AddControllers()
               .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly)
               .AddJsonOptions(options =>
               {
                   options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
               });
			services.Configure<ApiBehaviorOptions>(Options
                => Options.InvalidModelStateResponseFactory 
                = ApiResponseFactory.CustomValidationErrorResponse);

			services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


            return services;
        }
    }
}
