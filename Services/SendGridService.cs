using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Threading.Tasks;
using System.IO;

namespace MediSight_Project.Services
{
    public class SendGridService
    {
        public async Task SendEmail(string toEmail, string toName, string subject, string plainTextContent, string htmlContent)
        {
            try
            {
                var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("test@email.com", "Test Emmail");
                var to = new EmailAddress(toEmail, toName);
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

                using (var fileStream = File.OpenRead("~/favicon.ico"))
                {
                    await msg.AddAttachmentAsync("file.txt", fileStream);
                    var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                // handle error
            }

        }
    }

}