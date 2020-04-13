namespace Transferzor.Services
{
    public interface IEmailSender
    {
        void SendEmail(string to, string title, string body);
    }
}
