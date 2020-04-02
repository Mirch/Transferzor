using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transferzor.Models;

namespace Transferzor.Data
{
    public class TransferzorDbContext : DbContext
    {
        public TransferzorDbContext(DbContextOptions<TransferzorDbContext> options)
            : base(options)
        {

        }

        public DbSet<FileSendData> FileSendData { get; set; }
        public DbSet<FileStorageData> FileStorageData { get; set; }

    }
}
