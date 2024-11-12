using PuppeteerSharp;
using PuppeteerSharp.Media;
using System.Text.Json;

namespace Api.Processor
{
    public class CertificateProcessor
    {
        private readonly MySQLContext _context;

        public CertificateProcessor(MySQLContext context)
        {
            _context = context;
        }

        public async Task ProcessMessageAsync(string message)
        {
            var student = JsonSerializer.Deserialize<StudentModel>(message);

            Console.WriteLine($"Processando diploma para {student.Name}");

            var template = await File.ReadAllTextAsync("C:/Ana/PROJETOS INDIVIDUAIS/C#/certificate-issuer/worker/CertificateTemplate/certificate.html");

            var htmlContent = template
                .Replace("[[nome]]", student.Name)
                .Replace("[[curso]]", student.Course)
                .Replace("[[nacionalidade]]", student.Nationality)
                .Replace("[[estado]]", student.State)
                .Replace("[[data_nascimento]]", student.BirthDate)
                .Replace("[[documento]]", student.RG)
                .Replace("[[data_conclusao]]", student.ConclusionDate)
                .Replace("[[carga_horaria]]", student.WorkLoad)
                .Replace("[[data_emissao]]", student.IssueDate)
                .Replace("[[nome_assinatura]]", student.Sign)
                .Replace("[[cargo]]", student.Role);

            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                ExecutablePath = "C:/Program Files/Google/Chrome/Application/chrome.exe",
                Headless = true,
                Args = new[] { "--no-sandbox", "--disable-setuid-sandbox" }
            });

            var page = await browser.NewPageAsync();
            await page.SetContentAsync(htmlContent);

            var pdfBytes = await page.PdfDataAsync(new PdfOptions
            {
                Format = PaperFormat.A4,
                PrintBackground = true
            });

            await browser.CloseAsync();

            var pdfFilePath = await SavePdfAsync(pdfBytes, student.Name);

            Console.WriteLine($"Diploma gerado e salvo em {pdfFilePath}");
        }

        private async Task<string> SavePdfAsync(byte[] pdfBytes, string name)
        {
            var fileName = $"{name}.pdf";
            var filePath = "/app/pdfs";
            var pdfFilePath = $"{filePath}/{fileName}";
            var directoryPath = Path.GetDirectoryName(pdfFilePath);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            await File.WriteAllBytesAsync(pdfFilePath, pdfBytes);

            var certificate = CreateCertificateInstance(name, fileName, pdfFilePath);

            await SaveCertificateInstance(certificate);

            return pdfFilePath;
        }

        private CertificateFileModel CreateCertificateInstance(string name, string fileName, string filePath)
        {
            var certificateObject = new CertificateFileModel { Name = name, FileName = fileName, FilePath = filePath };
            return certificateObject;
        }

        private async Task<CertificateFileModel> SaveCertificateInstance(CertificateFileModel pdfFile)
        {
            var pdfSaved = _context.PdfFile.Add(pdfFile);
            var rowsAffected = await _context.SaveChangesAsync();

            if (rowsAffected < 1)
            {
                throw new Exception("It was not possible to save: " + pdfFile.FileName + " in database");
            }

            return pdfSaved.Entity;
        }
    }
}
