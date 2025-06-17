using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    internal class AppointmentWithPatientAndDoctor : Specifications<Appointment>
    {
        public AppointmentWithPatientAndDoctor // to get appointments with filtrations
            (int? doctorId, int? patientId,  AppointmentStatus? status) :
            base(a =>
            (doctorId == null || a.DoctorId == doctorId.Value) &&
            (patientId == null || a.PatientId == patientId.Value) &&
            (status == null || a.Status == status.Value)
        )
        {
            AddInclude(a => a.Patient);
            AddInclude(a => a.Doctor);
        }

        public AppointmentWithPatientAndDoctor() : base(null)
        {
			AddInclude(appointment => appointment.Doctor);
			AddInclude(appointment => appointment.Patient);

        }
        public AppointmentWithPatientAndDoctor( int DoctorId,AppointmentStatus status) 
            : base(appointment => appointment.DoctorId == DoctorId
            && appointment.Status==status
            )
        {
            AddInclude(appointment => appointment.Doctor);
            AddInclude(appointment => appointment.Patient);
           // AddInclude(appointment => appointment.Status);

        }
        public AppointmentWithPatientAndDoctor(int PatientId)
            :base(appointment=>appointment.Patient.Id==PatientId)
        {
            AddInclude(appointment=>appointment.Patient);
        }
    }
}
