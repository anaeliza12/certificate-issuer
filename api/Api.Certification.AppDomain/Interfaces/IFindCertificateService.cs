using Api.Certification.AppDomain.Model;

namespace Api.Certification.AppDomain.Interfaces
{
    public interface IFindCertificateService
    {
        Task<byte[]> FindCertificateAsync(string student);
    }
}
