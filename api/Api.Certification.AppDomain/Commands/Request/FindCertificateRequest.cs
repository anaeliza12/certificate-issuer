using Api.Certification.AppDomain.Model;
using MediatR;

namespace Api.Certification.AppDomain.Commands.request
{
    public class FindCertificateRequest : IRequest<FindCertificateResponse>
    {
        public string Name { get; set; }     
    }
}
