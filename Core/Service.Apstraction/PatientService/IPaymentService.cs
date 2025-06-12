
using Microsoft.Extensions.Configuration;
using Shared.AppointmentModels;

namespace Service.Abstraction.PatientService
{
	public interface IPaymentService
	{
		public Task<AppointmentDto> CreatePaymentIntentAsync(Guid appointmentId);
		public Task UpdatePaymentStatusAsync(string request, string header);

	}
}
