using System.ComponentModel.DataAnnotations;

namespace Transferzor.Models
{
    public class FileStorageData
    {
        [Key]
        public int FileSendDateId { get; set; }
        public FileSendData FileSendData { get; set; }

        [DataType(DataType.Url)]
        public string FileUri { get; set; }
    }
}
