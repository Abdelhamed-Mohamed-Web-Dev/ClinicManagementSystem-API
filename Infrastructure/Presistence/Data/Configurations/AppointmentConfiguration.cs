using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
	internal class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
	{
		public void Configure(EntityTypeBuilder<Appointment> builder)
		{
			builder.HasOne(a => a.Doctor)
				   .WithMany()
				   .HasForeignKey(a => a.DoctorId)
				   .OnDelete(DeleteBehavior.Restrict); // or Cascade if appropriate
		}
	}
}
