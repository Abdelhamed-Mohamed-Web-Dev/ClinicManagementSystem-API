using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.AdminModels
{
	public record UpdatePatientDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateOnly BirthDate { get; set; }
		public string Address { get; set; }
	}
}
