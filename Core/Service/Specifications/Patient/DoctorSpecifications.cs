
namespace Service.Specifications.Patient
{
	public class DoctorSpecifications : Specifications<Domain.Entities.Doctor>
	{
		public DoctorSpecifications(string? specialty, string? search)
			: base
			(d => (string.IsNullOrWhiteSpace(specialty) || d.Speciality == specialty) &&
			(string.IsNullOrWhiteSpace(search) || d.Name.ToUpper().Contains(search.ToUpper().Trim())))
		{
			AddInclude(d => d.Appointments);
		}
		public DoctorSpecifications(int id) : base(d => d.Id == id)
		{
			AddInclude(d => d.Appointments);
		}
		public DoctorSpecifications(string userName) : base(d => d.UserName==userName)
		{
			AddInclude(d => d.Appointments);
		}

	}
}
