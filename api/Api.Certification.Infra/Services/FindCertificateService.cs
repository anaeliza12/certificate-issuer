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
        private readonly IRedisService _redis;
        public FindCertificateService(MySQLContext Dbcontext, IRedisService redis)
        {
            _Dbcontext = Dbcontext;
            _redis = redis;
        }
        public async Task<byte[]> FindCertificateAsync(string studentName)
        {
            var key = $"certificate:{studentName}";
            var cachedCertificate = _redis.GetCacheAsync(key).Result;
            CertificateFileModel filePath;

            if (cachedCertificate.Any())
            {
                filePath = JsonConvert.DeserializeObject<CertificateFileModel>(cachedCertificate);
            }
            else
            {
                filePath = await _Dbcontext.PdfFile.Where(p => p.Name.Contains(studentName)).FirstOrDefaultAsync();

                if(filePath == null)
                {

                }
                var filePathJson = JsonConvert.SerializeObject(filePath);
                await _redis.InsertCacheAsync(key, filePathJson);
            }

            byte[] pdfBytes;

            if (filePath != null)
            {
                return pdfBytes = File.ReadAllBytes(filePath.FilePath);
            }

            return null;
        }
    }
}
