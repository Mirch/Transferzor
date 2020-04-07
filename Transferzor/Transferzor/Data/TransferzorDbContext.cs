using Microsoft.EntityFrameworkCore;
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
