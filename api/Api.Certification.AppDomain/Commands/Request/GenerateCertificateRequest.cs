using Api.Certification.AppDomain.Model;
using MediatR;

namespace Api.Certification.AppDomain.Commands.request
{
    public class GenerateCertificateRequest : IRequest<GenerateCertificateResponse>
    {
        public StudentModel StudentModel { get; set; }     
    }
}
