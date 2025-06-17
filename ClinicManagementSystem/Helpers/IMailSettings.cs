using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Helpers
{
    public interface IMailSettings
    {
        public void sendEmail(Email email); 
    }
}
