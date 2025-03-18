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
    internal class MedicalRecordConfigrations : IEntityTypeConfiguration<Medical_Record>
    {
        public void Configure(EntityTypeBuilder<Medical_Record> builder)
        {
            builder.HasMany(m => m.lap_Tests)
                .WithOne(m => m.Medical_Record)
                .HasForeignKey(m => m.MedicalId);
            builder.HasMany(m => m.Radioizations)
                .WithOne(m => m.medical_Record)
                .HasForeignKey(m => m.MedicalRecord_Id);
        }
    }
}
