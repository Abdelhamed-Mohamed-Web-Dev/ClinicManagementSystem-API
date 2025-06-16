using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
	internal class DoctorPictureResolver(IConfiguration configuration) : IValueResolver<Doctor, DoctorDto, string>
	{
		public string Resolve(Doctor source, DoctorDto destination, string destMember, ResolutionContext context)
		=> !string.IsNullOrWhiteSpace(source.PictureUrl) ?
			$"{configuration["BaseUrl"]}/{source.PictureUrl}" :
			string.Empty;
	}
}
//D:\Garduation Project\ClinicManagementSystem\ClinicManagementSystem\wwwroot\images\Dr. Ahmed Al-Mansoori.jpg.jpg
