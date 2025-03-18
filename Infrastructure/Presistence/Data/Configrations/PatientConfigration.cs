using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configrations
{
    internal class PatientConfigration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasMany(p => p.Appointments)
                .WithOne(p=>p.Patient)
                .HasForeignKey(p=>p.PatientId);
            builder.HasMany(p=>p.Medical_Records)
                .WithOne(p=>p.Patient)
                .HasForeignKey(p=>p.PatientId);
        }
    }
}
