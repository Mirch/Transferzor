using BlazorInputFile;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Transferzor.Models
{
    public class FileSendData
    {
        public int Id { get; set; }
        [DataType(DataType.EmailAddress)]
        public string SenderEmail { get; set; }
        [DataType(DataType.EmailAddress)]
        public string ReceiverEmail { get; set; }
        [NotMapped]
        public IFileListEntry File { get; set; }
    }
}
