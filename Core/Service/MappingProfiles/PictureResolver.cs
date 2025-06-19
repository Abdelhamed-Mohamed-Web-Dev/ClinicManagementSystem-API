using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
	internal class PictureResolver<TSource, TDestination>(IConfiguration configuration) : IValueResolver<TSource, TDestination, string>
	{
		// Must Have Property "PictureUrl" To Map It
		public string Resolve(TSource source, TDestination destination, string destMember, ResolutionContext context)
			=> !string.IsNullOrWhiteSpace(typeof(TSource).GetProperty("PictureUrl")?.GetValue(source).ToString()) ?
			$"{configuration["BaseUrl"]}/{typeof(TSource).GetProperty("PictureUrl")?.GetValue(source)}" :
			string.Empty;
	}
}
//D:\Garduation Project\ClinicManagementSystem\ClinicManagementSystem\wwwroot\images\Dr. Ahmed Al-Mansoori.jpg.jpg
