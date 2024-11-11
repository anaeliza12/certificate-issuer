using Api.Certification.AppDomain.Commands.request;
using Api.Certification.AppDomain.Interfaces;
using MediatR;

namespace Api.Certification.AppDomain.Handler
{
    public class FindCertificateHandler : IRequestHandler<FindCertificateRequest, FindCertificateResponse>
    {
        private IFindCertificateService _service { get; set; }
        public FindCertificateHandler(IFindCertificateService service)
        {
            _service = service;
        }

        public async Task<FindCertificateResponse> Handle(FindCertificateRequest request, CancellationToken cancellationToken)
        {
            var pdfBytes = await _service.FindCertificateAsync(request.Name);

            var response = new FindCertificateResponse
            {
                Certificate = pdfBytes
            };

            return response;
        }
    }
}
