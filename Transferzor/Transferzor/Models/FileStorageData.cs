using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
