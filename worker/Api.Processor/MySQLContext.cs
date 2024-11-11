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

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connection = "Server=DESKTOP-QIJ7H6I;Port=3306;Database=studentscertificate";
            options.UseMySql(connection, ServerVersion.AutoDetect(connection));
        }
    }
}
