
namespace Domain.Entities
{
    public class Patient : BaseEntity<int>
	{
        public string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Gender { get; set; }
        public string? Address { get; set; }
        public string Phone { get; set; }
   //     public string UserName { get; set; }
        public string Email { get; set; }
        // محتاجين نضيف ال Last Visit 
        public string UserId { get; set; }
     //   public User user { get; set; }
        public ICollection<Appointment>? Appointments { get; set; } = new List<Appointment>();
        public ICollection<MedicalRecord>? MedicalRecords { get; set; } = new HashSet<MedicalRecord>();
      //  public ICollection<Doctor_Rate> rates { get; set; } = new List<Doctor_Rate>();
        public ICollection<FavoriteDoctors>? FavoriteDoctors { get; set; }= new List<FavoriteDoctors>();

    }
}
