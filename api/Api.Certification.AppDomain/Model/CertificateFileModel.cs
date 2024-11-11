using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Certification.AppDomain.Model
{
    [Table("certificate_files_db")]
    public class CertificateFileModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}
