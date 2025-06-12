namespace Shared.AppointmentModels
{
    public record AvailableDaysDto
    {
        public DateTime Date { get; set; }
        public string DayName { get; set; }
        public string DateString => Date.ToString("yyyy-MM-dd");
        public bool IsAvailable { get; set; }
        //public int DayNumber { get; set; } // 0-6 (Sunday-Saturday)
    }
}
