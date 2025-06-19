using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications.Patient
{
	internal class FavDoctorsSpecifications : Specifications<FavoriteDoctors>
	{
		public FavDoctorsSpecifications(int patientId) : base(n => n.PatientId == patientId)
		{
		}
	}
}
