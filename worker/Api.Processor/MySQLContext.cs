using Microsoft.EntityFrameworkCore;

namespace Api.Processor
{
    public class MySQLContext : DbContext
    {
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options)
        {
        }
        public DbSet<StudentModel> Student { get; set; }
        public DbSet<CertificateFileModel> PdfFile { get; set; }
    }
}
