using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Transferzor.Services;

namespace Transferzor.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileHandler _fileHandler;

        public FilesController(
            IFileHandler fileHandler)
        {
            _fileHandler = fileHandler;
        }

        [HttpGet("{fileName}")]
        public async Task<IActionResult> GetBlobDownload([FromRoute]string fileName)
        {
            var file = await _fileHandler.DownloadFileAsync(fileName);

            if (file.Content == null)
            {
                return Redirect("/");
            }

            var content = new System.IO.MemoryStream(file.Content);
            var contentType = "application/octet-stream";
            return File(content, contentType, file.Name);
        }
    }
}