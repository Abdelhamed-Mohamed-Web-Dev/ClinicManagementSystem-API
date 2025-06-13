using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications.Patient
{
	public class AppointmentWithPaymentIntentSpecifications:Specifications<Appointment>
	{
		public AppointmentWithPaymentIntentSpecifications(string PaymentIntentId) 
			: base(a => a.PaymentIntentId == PaymentIntentId)
		{
		}
	}
}
