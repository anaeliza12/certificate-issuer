using Api.Certification.AppDomain.Commands.request;
using Api.Certification.AppDomain.Model;

namespace Api.Certification.AppDomain.Interfaces
{
    public interface IGenerateCertificateService
    {
        Task GenerateCertificateAsync(GenerateCertificateRequest request);
        Task<StudentModel> SaveCertificateStudentAsync(StudentModel student);
    }
}
