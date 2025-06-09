
namespace ClinicManagementSystem.Factories
{
	public class ApiResponseFactory
	{
		public static IActionResult CustomValidationErrorResponse(ActionContext context)
		{
			// Get All Errors From Model State
			var errors = context.ModelState.Where(Error => Error.Value.Errors.Any()).
				Select(error => new ValidationError
				{
					Field = error.Key,
					ErrorsDescription = error.Value.Errors.Select(e => e.ErrorMessage)
				});
			// Create Response "شكل الريترن اوبجكت"
			var response = new ValidationErrorResponse()
			{
				StatusCode = (int)HttpStatusCode.BadRequest,
				ErrorMassage = "Validation Error!",
				Errors = errors
			};
			// Return
			return new BadRequestObjectResult(response);
		}
	}
}
