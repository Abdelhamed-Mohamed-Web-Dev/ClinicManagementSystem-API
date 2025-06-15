using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications.Patient
{
	internal class PatientBySearchSpecifications : Specifications<Domain.Entities.Patient>
	{
		public PatientBySearchSpecifications(string? search) :
			base(p => 
			string.IsNullOrWhiteSpace(search) || 
			p.Name.ToUpper().Contains(search.ToUpper().Trim()))
		{
			AddInclude(p => p.Appointments);
			AddInclude(p => p.MedicalRecords);
		}
	}
}
