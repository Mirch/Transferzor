using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Transferzor.Models;

namespace Transferzor.Services
{
    public class AwsS3FileManager : IAwsS3FileManager
    {
        private readonly IAmazonS3 _client;
        private readonly string _bucket;

        public AwsS3FileManager(
            IAmazonS3 client)
        {
            _client = client;
            _bucket = "transferzor-files";
        }

        public async Task<string> UploadFileAsync(string fileName, Stream file)
        {
            var filestream = new MemoryStream();
            await file.CopyToAsync(filestream);

            var s3FileName = $"{DateTime.Now.Ticks}-{fileName}";

            var transferRequest = new TransferUtilityUploadRequest()
            {
                ContentType = "application/zip",
                InputStream = filestream,
                BucketName = _bucket,
                Key = s3FileName
            };
            transferRequest.Metadata.Add("x-amz-meta-title", fileName);

            var fileTransferUtility = new TransferUtility(_client);
            await fileTransferUtility.UploadAsync(transferRequest);

            return s3FileName;
        }

        public async Task<TransferFile> DownloadFileAsync(string fileName)
        {
            var request = new GetObjectRequest
            {
                BucketName = _bucket,
                Key = fileName
            };

            using (var objectResponse = await _client.GetObjectAsync(request))
            {
                if (objectResponse.HttpStatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception("Could not find file.");
                }

                using (var responseStream = objectResponse.ResponseStream)
                using (var reader = new StreamReader(responseStream))
                {
                    var result = new MemoryStream();
                    responseStream.CopyTo(result);
                    return new TransferFile
                    {
                        Name = fileName,
                        Content = result.ToArray()
                    };
                }
            }
        }

        public async Task DeleteFileAsync(string fileName)
        {
            try
            {
                var deleteObjectRequest = new DeleteObjectRequest
                {
                    BucketName = _bucket,
                    Key = fileName
                };

                var result = await _client.DeleteObjectAsync(deleteObjectRequest);
            }
            catch (AmazonS3Exception)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
