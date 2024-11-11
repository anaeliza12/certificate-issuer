using Api.Certification.AppDomain.Commands.request;
using MediatR;

namespace Api.Certification.AppDomain.Handler
{
    public class FindCertificateHandler : IRequestHandler<FindCertificateRequest, FindCertificateResponse>
    {
        Task<FindCertificateResponse> IRequestHandler<FindCertificateRequest, FindCertificateResponse>.Handle(FindCertificateRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
