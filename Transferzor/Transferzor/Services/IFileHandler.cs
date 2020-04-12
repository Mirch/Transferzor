using System.Threading.Tasks;
using Transferzor.Models;

namespace Transferzor.Services
{
    public interface IFileHandler
    {
        Task UploadFileAsync(FileSendData fileSendData);
        Task<TransferFile> DownloadFileAsync(string fileName);
    }
}
