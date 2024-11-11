using Api.Certification.AppDomain.Commands.request;
using MediatR;
using Api.Certification.AppDomain.Interfaces;
using Api.Certification.AppDomain.Model;
using Api.Certification.AppDomain.Utils;
using Api.Certification.AppDomain.Utils.AppSettings;
using System.Xml.Linq;

namespace Api.Certification.AppDomain.Handler
{
    public class GenerateCertificateHandler: IRequestHandler<GenerateCertificateRequest, GenerateCertificateResponse>
    {
        private readonly IGenerateCertificateService _generateService;
        private readonly TemplateConfig _templateConfig;

        public GenerateCertificateHandler(IGenerateCertificateService generateService, TemplateConfig template)
        {
            _generateService = generateService;
            _templateConfig = template;
        }

        public async Task<GenerateCertificateResponse> Handle(GenerateCertificateRequest request, CancellationToken cancellationToken)
        {
            await _generateService.GenerateCertificateAsync(request);
            //var response = new GenerateCertificateResponse()
            //{
            //    Certificate = pdfBytes
            //};

            //var PdfFile = new CertificateFileModel()
            //{
            //    FileName = request.StudentModel.GeneratePdfFileName(),
            //    FilePath = _templateConfig.FilePath
            //};

            //await File.WriteAllBytesAsync(pdfFilePath, pdfBytes);

            //var student = await _generateService.SaveStudentAsync(request.StudentModel);
            //var certificate = await _generateService.SaveCertificateAsync(PdfFile);

            return new GenerateCertificateResponse();
        }
    }
}
