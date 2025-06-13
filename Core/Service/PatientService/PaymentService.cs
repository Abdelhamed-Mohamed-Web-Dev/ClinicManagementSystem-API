using Domain.Exceptions;
using Domain.Exceptions.NotFoundExceptions;
using Microsoft.Extensions.Configuration;
using Shared.AppointmentModels;
using Stripe;
using Stripe.Climate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentType = Domain.Entities.AppointmentType;

namespace Service.PatientService
{
	internal class PaymentService(IUnitOfWork unitOfWork,
		IConfiguration configuration,
		IMapper mapper) : IPaymentService
	{
		public async Task<AppointmentDto> CreatePaymentIntentAsync(Guid appointmentId)
		{
			StripeConfiguration.ApiKey = configuration.GetRequiredSection("Stripe")["SecretKey"];

			var appointment = await unitOfWork.GetRepository<Appointment, Guid>().GetAsync(new AppointmentSpecifications(appointmentId));
			if (appointment is null) throw new AppointmentNotFoundException(appointmentId);

			var doctor = await unitOfWork.GetRepository<Doctor, int>().GetAsync(appointment.DoctorId);
			if (doctor is null) throw new DoctorNotFoundException(appointment.DoctorId);

			var amount = appointment.Type == AppointmentType.NewVisit ?
				   doctor.NewVisitPrice : doctor.FollowUpVisitPrice;

			var service = new PaymentIntentService();
			PaymentIntent paymentIntent;
			var options = new PaymentIntentCreateOptions
			{
				Amount = (long)amount * 100,
				Currency = "USD",
				PaymentMethodTypes = new List<string>() { "card" }
			};
			paymentIntent = await service.CreateAsync(options);

			appointment.ClintSecret = paymentIntent.ClientSecret;
			appointment.PaymentIntentId = paymentIntent.Id;

			unitOfWork.GetRepository<Appointment,Guid>().Update(appointment);
			return mapper.Map<AppointmentDto>(appointment);
		}

		public async Task UpdatePaymentStatusAsync(string request, string header)
		{
			var endPoint = configuration.GetRequiredSection("Stripe")["WebhookSecret"];
			var stripeEvent = EventUtility.ConstructEvent(request, header, endPoint);
			var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

			switch (stripeEvent.Type)
			{
				case EventTypes.PaymentIntentPaymentFailed:
					await UpdatePaymentIntentFailed(paymentIntent.Id);
					break;
				case EventTypes.PaymentIntentSucceeded:
					await UpdatePaymentIntentSucceeded(paymentIntent.Id);
					break;
			}
		}

		private async Task UpdatePaymentIntentFailed(string id)
		{
			var appointment = await unitOfWork.GetRepository<Appointment, Guid>()
				.GetAsync(new AppointmentWithPaymentIntentSpecifications(id))
				?? throw new NotFoundException("Appointment Not Found!");
			appointment.Status = (Domain.Entities.AppointmentStatus)PaymentStatus.Failed;
			unitOfWork.GetRepository<Appointment, Guid>().Update(appointment);
			await unitOfWork.SaveChangesAsync();
		}

		private async Task UpdatePaymentIntentSucceeded(string id)
		{
			var appointment = await unitOfWork.GetRepository<Appointment, Guid>()
				.GetAsync(new AppointmentWithPaymentIntentSpecifications(id))
				?? throw new NotFoundException("Appointment Not Found!");
			appointment.Status = (Domain.Entities.AppointmentStatus)PaymentStatus.Received;
			unitOfWork.GetRepository<Appointment, Guid>().Update(appointment);
			await unitOfWork.SaveChangesAsync();
		}
	}
}
