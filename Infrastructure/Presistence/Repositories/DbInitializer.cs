using Microsoft.AspNetCore.Identity;
using Shared.DoctorModels;

namespace Persistence.Repositories
{
    public class DbInitializer : IDbInitializer
    {
        readonly MainContext mainContext;
        readonly UserManager<User> userManager;
        readonly RoleManager<IdentityRole> roleManager;
        public DbInitializer(MainContext mainContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.mainContext = mainContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task InitializeAsync()
        {

            try
            {
                // Check Pending Migrations

                if (mainContext.Database.GetPendingMigrations().Any())
                    await mainContext.Database.MigrateAsync();

                // Data Seeding

                // Doctors
                if (!mainContext.Doctors.Any())
                {
                    // Read As A String
                    var doctorsFile = await File.ReadAllTextAsync(@"..\Infrastructure\Presistence\Data\Seeding\Doctors.json");
                    // Transform To Obj
                    var doctors = JsonSerializer.Deserialize<List<Doctor>>(doctorsFile);
                    // Add To DB & SaveChanges
                    if (doctors is not null && doctors.Any())
                    {
                        await mainContext.Doctors.AddRangeAsync(doctors);
                        await mainContext.SaveChangesAsync();
                    }
                }
                // Patients
                if (!mainContext.Patients.Any())
                {
                    // Read As A String
                    var patientsFile = await File.ReadAllTextAsync(@"..\Infrastructure\Presistence\Data\Seeding\Patients.json");
                    // Transform To Obj
                    var patients = JsonSerializer.Deserialize<List<Patient>>(patientsFile);
                    // Add To DB & SaveChanges
                    if (patients is not null && patients.Any())
                    {
                        await mainContext.Patients.AddRangeAsync(patients);
                        await mainContext.SaveChangesAsync();
                    }
                }
                // MedicalRecords
                if (!mainContext.MedicalRecords.Any())
                {
                    // Read As A String
                    var medicalRecordsFile = await File.ReadAllTextAsync(@"..\Infrastructure\Presistence\Data\Seeding\MedicalRecords.json");
                    // Transform To Obj
                    var medicalRecords = JsonSerializer.Deserialize<List<MedicalRecord>>(medicalRecordsFile);
                    // Add To DB & SaveChanges
                    if (medicalRecords is not null && medicalRecords.Any())
                    {
                        await mainContext.MedicalRecords.AddRangeAsync(medicalRecords);
                        await mainContext.SaveChangesAsync();
                    }
                }
                // LapTests
                if (!mainContext.LapTests.Any())
                {
                    // Read As A String
                    var lapTestsFile = await File.ReadAllTextAsync(@"..\Infrastructure\Presistence\Data\Seeding\LapTests.json");
                    // Transform To Obj
                    var lapTests = JsonSerializer.Deserialize<List<LapTest>>(lapTestsFile);
                    // Add To DB & SaveChanges
                    if (lapTests is not null && lapTests.Any())
                    {
                        await mainContext.LapTests.AddRangeAsync(lapTests);
                        await mainContext.SaveChangesAsync();
                    }
                }
                // Radiations
                if (!mainContext.Radiations.Any())
                {
                    // Read As A String
                    var radiationsFile = await File.ReadAllTextAsync(@"..\Infrastructure\Presistence\Data\Seeding\Radiations.json");
                    // Transform To Obj
                    var radiations = JsonSerializer.Deserialize<List<Radiology>>(radiationsFile);
                    // Add To DB & SaveChanges
                    if (radiations is not null && radiations.Any())
                    {
                        await mainContext.Radiations.AddRangeAsync(radiations);
                        await mainContext.SaveChangesAsync();
                    }
                }
                // Appointments
                if (!mainContext.Appointments.Any())
                {
                    // Read As A String
                    var appointmentsFile = await File.ReadAllTextAsync(@"..\Infrastructure\Presistence\Data\Seeding\Appointments.json");
                    // Transform To Obj
                    var appointments = JsonSerializer.Deserialize<List<Appointment>>(appointmentsFile);
                    // Add To DB & SaveChanges
                    if (appointments is not null && appointments.Any())
                    {
                        await mainContext.Appointments.AddRangeAsync(appointments);
                        await mainContext.SaveChangesAsync();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task InitializeIdentityAsync()
        {
            // Seed Defult Role
            //if(!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole("Doctor"));
                await roleManager.CreateAsync(new IdentityRole("Patient"));
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // Seed Defult User
           // if (!userManager.Users.Any())
            {
                var Doctor1 = new User
                {
                    DisplayName = "Ahmed",
                    Email = "Ahmed@gmail.com",
                    UserName = "D01",
                    PhoneNumber = "0102546145",

                };
                var Patient1 = new User
                {
                    DisplayName = "Ali",
                    Email = "Ali@gmail.com",
                    UserName = "P001",
                    PhoneNumber = "0112545145",

                };
                var Admin1 = new User
                {
                    DisplayName = "Mohamed",
                    Email = "mohamed@gmail.com",
                    UserName = "A01",
                    PhoneNumber = "011235165"

                };


                await userManager.CreateAsync(Doctor1,"12345678");
                await userManager.CreateAsync(Patient1,"12345678");
                await userManager.CreateAsync(Admin1,"12345678");
                await userManager.AddToRoleAsync(Doctor1, "Doctor");
                await userManager.AddToRoleAsync(Patient1, "Patient");
                await userManager.AddToRoleAsync(Admin1, "Admin");
                
            //}
        }
    }
}
  