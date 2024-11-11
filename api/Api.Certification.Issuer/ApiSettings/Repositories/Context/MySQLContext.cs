using Api.Certification.AppDomain.Model;
using Api.Certification.AppDomain.Utils.AppSettings;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Api.Certification.Infra.ApiSettings.Repositories.Context
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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudentModel>()
       .ToTable("students_db");

            modelBuilder.Entity<StudentModel>()
                .HasKey(s => s.Id);
        }
    }
}
