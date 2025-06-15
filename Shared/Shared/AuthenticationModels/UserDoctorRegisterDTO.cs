using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.AuthenticationModels
{
    public record UserDoctorRegisterDTO :UserRegisterDTO
    {
        public string Speciality { get; set; }
        public string? PictureUrl { get; set; }
        public int? Rate { get; set; }
        public string? Bio { get; set; }
        public decimal NewVisitPrice { get; set; }
        public decimal FollowUpVisitPrice { get; set; }
        public ICollection<int> WorkingDays { get; set; } = new List<int>();
        public TimeSpan StartTime { get; set; } = new TimeSpan(13, 0, 0); // 1 PM
        public TimeSpan EndTime { get; set; } = new TimeSpan(20, 0, 0); // 8 PM
        public int AppointmentDuration { get; set; } = 30; // 30 Minutes

    }
}
