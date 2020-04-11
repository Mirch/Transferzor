using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transferzor.Data;
using Transferzor.Models;

namespace Transferzor.Services
{
    public class FileHandler : IFileHandler
    {
        private readonly IAwsS3FileManager _s3FileManager;
        private readonly TransferzorDbContext _context;

        public FileHandler(
            TransferzorDbContext context,
            IAwsS3FileManager s3FileManager)
        {
            _context = context;
            _s3FileManager = s3FileManager;
        }

        public async Task<TransferFile> DownloadFile(string fileName)
        {
            var dbFileData = await _context
                .FileStorageData
                    .Include(f => f.FileSendData)
                .SingleOrDefaultAsync(f => f.FileName == fileName);

            if (dbFileData == null)
            {
                return new TransferFile();
            }

            var file = await _s3FileManager.DownloadFileAsync(fileName);

            _context.FileSendData.Remove(dbFileData.FileSendData);
            _context.FileStorageData.Remove(dbFileData);
            await _context.SaveChangesAsync();

            return file;
        }

        public async Task UploadFileAsync(FileSendData fileSendData)
        {
            _context.FileSendData.Add(fileSendData);

            var s3fileName = await _s3FileManager.UploadFileAsync(fileSendData.File.Name, fileSendData.File.Data);

            _context.FileStorageData.Add(new FileStorageData
            {
                FileSendDateId = fileSendData.Id,
                FileName = s3fileName
            });
            await _context.SaveChangesAsync();
        }
    }
}
