using System.IO;
using System.Threading.Tasks;
using Transferzor.Models;

namespace Transferzor.Services
{
    public interface IAwsS3FileManager
    {
        Task<string> UploadFileAsync(string fileName, Stream file);
        Task<TransferFile> DownloadFileAsync(string fileName);
    }
}
