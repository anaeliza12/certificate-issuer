using Api.Certification.AppDomain.Model;
using Api.Certification.AppDomain.Utils.AppSettings;
using Microsoft.EntityFrameworkCore;

namespace Api.Certification.Infra.ApiSettings.Repositories.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext(DbContextOptions<MySQLContext> options, DBConfig config) : base(options)
        {
            _config = config;
        }

        private DBConfig _config;
        public DbSet<StudentModel> Student { get; set; }
        public DbSet<CertificateFileModel> PdfFile { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connection = _config.DefaultConnection;
            options.UseMySql(connection, ServerVersion.AutoDetect(connection));
        }

    }
}
