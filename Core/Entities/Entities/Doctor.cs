
namespace Domain.Entities
{
    public class Doctor : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Speciality { get; set; }
        // محتاجين نضيف Notification ( 2 Variable ( Type ,Content)   )
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
