using Microsoft.AspNetCore.Mvc;
using Shared.AppointmentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
	public class PaymentController(IServiceManager serviceManager) : APIController
	{
		[HttpPost("CreatePayment")]
		public async Task<ActionResult<AppointmentDto>> CreatePaymentIntent([FromQuery] Guid appointmentId)
			=> Ok(await serviceManager.PaymentService().CreatePaymentIntentAsync(appointmentId));

		[HttpPost("WebHook")]
		public async Task<IActionResult> WebHook()
		{
			var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
			await serviceManager.PaymentService().UpdatePaymentStatusAsync(json, Request.Headers["Stripe-Signature"]!);
			return new EmptyResult();
		}
	}
}
