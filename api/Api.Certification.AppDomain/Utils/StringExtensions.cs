using Api.Certification.AppDomain.Model;

namespace Api.Certification.AppDomain.Utils
{
    public static class StringExtensions
    {
        public static string GeneratePdfFileName(this StudentModel student)
        {
            var pdf = ".pdf";
            var pdfFileName = student.Name.Replace(" ", "") + pdf;

            return pdfFileName;
        }
    }
}
