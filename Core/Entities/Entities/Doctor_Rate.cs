//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Domain.Entities
//{
//    public class Doctor_Rate:BaseEntity<int>
//    {
//   //     public int Id { get; set; }
//        [Required]
//        public int DoctorId { get; set; }
//        public Doctor doctor { get; set; }
//        [Required]
//        public int PatientId { get; set; }
//        public Patient patient { get; set; }
        
//        public Appointment? appointment { get; set; }
//        public int? AppointmentId { get; set; }
       
//        [Range(1,5)]
//        public int Rating { get; set; }
//        public string? Comment { get; set; }

//    }
//}
