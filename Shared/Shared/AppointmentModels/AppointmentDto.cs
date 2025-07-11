﻿namespace Shared.AppointmentModels
{
    public record AppointmentDto
    {
        public Guid Id { get; set; }
        //public DateOnly Date { get; set; }
        //public TimeOnly Time { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public bool IsBooked { get; set; }
        public string? Notes { get; set; }
        public AppointmentType Type { get; set; } // كشف جديد او متابعه (استشارة) 0
        public AppointmentStatus Status { get; set; } // . حالة الكشف تم او لسه او اتلغى 

        //public Patient Patient { get; set; }
        public string PatientName { get; set; }
        //public Doctor Doctor { get; set; }
        public string DoctorName { get; set; }

    }
    public enum AppointmentStatus
    {
        Confirmed = 0,
        Pending = 1,
        Canceled = 2,
    }
    public enum AppointmentType
    {
        NewVisit = 0,
        FollowUpVisit = 1,
    }
}