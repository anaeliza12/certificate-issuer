using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Certification.AppDomain.Model
{
    [Table("students_db")]
    public class StudentModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public string State { get; set; }
        public string BirthDate { get; set; }
        public string RG { get; set; }
        public string ConclusionDate { get; set; }
        public string IssueDate { get; set; }
        public string Course { get; set; }
        public string WorkLoad { get; set; }
        public string Sign { get; set; }
        public string Role { get; set; }
    }
}
