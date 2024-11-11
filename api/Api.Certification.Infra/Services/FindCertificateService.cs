using Api.Certification.AppDomain.Commands.request;
using Api.Certification.AppDomain.Interfaces;
using Api.Certification.AppDomain.Model;
using Api.Certification.Infra.ApiSettings.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Api.Certification.Infra.Services
{
    public class FindCertificateService : IFindCertificateService
    {
        private readonly MySQLContext _Dbcontext;
        public FindCertificateService(MySQLContext Dbcontext)
        {
            _Dbcontext = Dbcontext;
        }
        public async Task<byte[]> FindCertificateAsync(string studentName)
        {
            var filePath = await _Dbcontext.PdfFile.Where(p => p.Name.Contains(studentName)).FirstOrDefaultAsync();
            byte[] pdfBytes;

            if(filePath != null)
            {
                return pdfBytes = File.ReadAllBytes(filePath.FilePath);         
            }

            return null;
        }
    }
}
