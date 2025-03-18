using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateOnly BirthDate { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<Medical_Record> Medical_Records { get; set; } = new HashSet<Medical_Record>();

    }
}
