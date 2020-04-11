using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transferzor.Models;

namespace Transferzor.Services
{
    public interface IFileHandler
    {
        Task UploadFileAsync(FileSendData fileSendData);
        Task<TransferFile> DownloadFile(string fileName);
    }
}
