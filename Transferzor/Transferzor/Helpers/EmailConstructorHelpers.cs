namespace Transferzor.Helpers
{
    public class EmailConstructorHelpers
    {
        private const string _uri = "https://localhost:44336";

        public static string CreateNewFileReceivedEmailBody(string fileName, string from)
        {
            return $"You received a new file from {from}. Go to {_uri}/files/{fileName} to download it.";
        }
    }
}
