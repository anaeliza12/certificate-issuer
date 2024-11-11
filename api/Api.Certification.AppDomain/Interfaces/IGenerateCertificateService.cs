using Api.Certification.AppDomain.Commands.request;
using Api.Certification.AppDomain.Model;

namespace Api.Certification.AppDomain.Interfaces
{
    public interface IGenerateCertificateService
    {
        Task GenerateCertificateAsync(GenerateCertificateRequest request);
        //Task SaveCertificateStudentAsync(PdfFileModel PdfFile);
        Task<StudentModel> SaveCertificateStudentAsync(StudentModel student);
        //Task<StudentModel> FindCertificateAsync(StudentModel student);
    }
}
