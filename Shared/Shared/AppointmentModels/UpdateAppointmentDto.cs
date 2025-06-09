namespace Shared.AppointmentModels
{
	public record UpdateAppointmentDto
	{
		public Guid Id { get; set; }
		public DateTime AppointmentDateTime { get; set; }
	}
}
