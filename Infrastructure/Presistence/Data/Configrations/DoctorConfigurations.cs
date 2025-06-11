namespace Persistence.Data.Configurations
{
    internal class DoctorConfigurations : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasMany(d => d.Appointments)
                .WithOne(d => d.Doctor)
                .HasForeignKey(d => d.DoctorId);
            builder.HasMany(d => d.doctor_Rates)
                .WithOne(d => d.doctor)
                .HasForeignKey(d => d.DoctorId);
            builder.Property(d => d.NewVisitPrice).HasColumnType("decimal(18,2)");
            builder.Property(d => d.FollowUpVisitPrice).HasColumnType("decimal(18,2)");
        }
    }
}
