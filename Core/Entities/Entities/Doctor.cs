
namespace Domain.Entities
{
    public class Doctor : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Speciality { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
