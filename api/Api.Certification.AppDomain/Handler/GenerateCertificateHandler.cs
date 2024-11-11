using Api.Certification.AppDomain.Commands.request;
using Api.Certification.AppDomain.Interfaces;
using Api.Certification.AppDomain.Utils.AppSettings;
using MediatR;

namespace Api.Certification.AppDomain.Handler
{
    public class GenerateCertificateHandler: IRequestHandler<GenerateCertificateRequest, GenerateCertificateResponse>
    {
        private readonly IGenerateCertificateService _generateService;

        public GenerateCertificateHandler(IGenerateCertificateService generateService)
        {
            _generateService = generateService;
        }

        public async Task<GenerateCertificateResponse> Handle(GenerateCertificateRequest request, CancellationToken cancellationToken)
        {
            await _generateService.GenerateCertificateAsync(request);

            var response = new GenerateCertificateResponse
            {
                Message = "Data received and processed successfully"
            };

            return response;
        }
    }
}
