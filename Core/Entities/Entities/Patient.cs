﻿
namespace Domain.Entities
{
    public class Patient : BaseEntity<int>
	{
        public string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        // محتاجين نضيف ال Last Visit 
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<MedicalRecord> MedicalRecords { get; set; } = new HashSet<MedicalRecord>();
        public ICollection<Doctor_Rate> rates { get; set; } = new List<Doctor_Rate>();
        public ICollection<FavoriteDoctors> FavoriteDoctors { get; set; }= new List<FavoriteDoctors>();

    }
}
