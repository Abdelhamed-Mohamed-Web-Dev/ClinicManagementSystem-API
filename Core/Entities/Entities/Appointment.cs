
using System.Runtime.Serialization;

namespace Domain.Entities
{
	public class Appointment : BaseEntity<Guid>
	{
        public DateTime AppointmentDateTime { get; set; }
        public bool IsBooked { get; set; }
        public string? Notes { get; set; }
        public AppointmentType Type { get; set; } // كشف جديد او متابعه (استشارة) 0
		public AppointmentStatus Status { get; set; } // . حالة الكشف تم او لسه او اتلغى 

        public string? ClintSecret { get; set; }
		public string? PaymentIntentId { get; set; }
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
        public decimal Amount { get; set; }

        public Patient Patient { get; set; }
		public int PatientId { get; set; }
		public Doctor Doctor { get; set; }
		public int DoctorId { get; set; }
	}

	public enum PaymentStatus
	{
		[EnumMember(Value = "Pending")]
		Pending = 0,
		[EnumMember(Value = "Received")]
		Received = 1,
		[EnumMember(Value = "Failed")]
		Failed = 2,
	}

	public enum AppointmentStatus
	{
		[EnumMember(Value = "Confirmed")]
		Confirmed = 0,
		[EnumMember(Value = "Pending")]
		Pending = 1,
		[EnumMember(Value = "Canceled")]
		Canceled = 2,
	}
	public enum AppointmentType
	{
		[EnumMember(Value = "NewVisit")]
		NewVisit = 0,
		[EnumMember(Value = "FollowUpVisit")]
		FollowUpVisit = 1,
	}
}
