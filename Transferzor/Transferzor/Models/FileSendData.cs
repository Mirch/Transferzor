using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Transferzor.Models
{
    public class FileSendData
    {
        public int Id { get; set; }
        [DataType(DataType.EmailAddress)]
        public string SenderEmail { get; set; }
        [DataType(DataType.EmailAddress)]
        public string ReceiverEmail { get; set; }
    }
}
