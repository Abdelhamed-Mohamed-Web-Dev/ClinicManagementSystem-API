using System.ComponentModel.DataAnnotations;

namespace Shared.AppointmentModels
{
    public record CreateAppointmentDto
    {
        [Required]
        public DateTime AppointmentDateTime { get; set; }
        [Required]
        public AppointmentType Type { get; set; }
        [Required]
        public int PatientId { get; set; }
        [Required]
        public int DoctorId { get; set; }
    }
}
