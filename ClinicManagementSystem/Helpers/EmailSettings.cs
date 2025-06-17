
using ClinicManagementSystem.Settings;
using MailKit.Net.Smtp;
using MimeKit;
using System.Net.Mail;

namespace ClinicManagementSystem.Helpers
{
    public class EmailSettings(IOptions<MailSettings> options) : IMailSettings
    {
        public void sendEmail(Email email)
        {
            var mail = new MimeMessage
            {
                Sender = MailboxAddress.Parse(options.Value.Email),
                Subject = email.Subject
            };
            mail.To.Add(MailboxAddress.Parse(email.To));
            mail.From.Add(new MailboxAddress(options.Value.DisplayName,options.Value.Email));
            var builder = new BodyBuilder();
            builder.TextBody= email.Body;
            mail.Body=builder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(options.Value.Host, options.Value.Prot, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(options.Value.Email,options.Value.Password);
            smtp.Send(mail);
            smtp.Disconnect(true);

        }
    }
}
