using System.Net;
using System.Net.Mail;

namespace Transferzor.Services
{
    public class GmailEmailSender : IEmailSender
    {
        private readonly AwsParameterStoreClient _awsParameterStore;

        public GmailEmailSender(
            AwsParameterStoreClient awsParameterStore)
        {
            _awsParameterStore = awsParameterStore;
        }

        public async void SendEmail(string to, string title, string body)
        {
            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.UseDefaultCredentials = false;
                client.EnableSsl = true;

                var username = await _awsParameterStore.GetValueAsync("SMTP-username");
                var password = await _awsParameterStore.GetValueAsync("SMTP-password");

                client.Credentials = new NetworkCredential(username, password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(username);
                mailMessage.To.Add(to);
                mailMessage.Subject = title;
                mailMessage.Body = body;

                client.Send(mailMessage);
            }
        }
    }
}
