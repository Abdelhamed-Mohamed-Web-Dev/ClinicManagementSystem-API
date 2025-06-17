using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Notifications :BaseEntity<int>
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; } = false;
        public NotificationType Type { get; set; } 
        public int? PatientId { get; set; }
        public Patient patient { get; set; }
        public int? DoctorId { get; set; }
        public Doctor doctor { get; set; }


    }
    public enum NotificationType
    {
        AppointmentBooked,
        AppointmentCancelled,
        TestResultReady,
        General
    }
}
