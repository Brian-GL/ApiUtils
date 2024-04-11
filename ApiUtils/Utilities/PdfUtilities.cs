using ApiUtils.Extensions;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf.Security;

namespace ApiUtils.Utilities
{
    /// <summary>
    /// Pdf utilities functions
    /// </summary>
    public static class PdfUtilities
    {
        /// <summary>
        /// Encrypts pdf from <see cref="MemoryStream"/> value
        /// </summary>
        /// <param name="memoryStream">Pdf stream</param>
        /// <param name="ownerPassword">Owner password to set</param>
        /// <param name="userPassword">User password to set</param>
        /// <returns>New <see cref="byte"/> array from pdf data encrypted</returns>
        /// <remarks>If <paramref name="ownerPassword"/> and <paramref name="userPassword"/> are null or empty pdf file won't be secured</remarks>
        public static byte[] Encrypt(MemoryStream? memoryStream, string? ownerPassword, string? userPassword)
        {
            byte[] pdfData = [];

            if (memoryStream is not null)
            {
                using PdfDocument pdfDocument = PdfReader.Open(stream: memoryStream!);

                PdfSecuritySettings pdfSecuritySettings = pdfDocument.SecuritySettings;
                pdfSecuritySettings.PermitAnnotations = false;
                pdfSecuritySettings.PermitAssembleDocument = false;
                pdfSecuritySettings.PermitExtractContent = false;
                pdfSecuritySettings.PermitFormsFill = true;
                pdfSecuritySettings.PermitFullQualityPrint = true;
                pdfSecuritySettings.PermitModifyDocument = false;
                pdfSecuritySettings.PermitPrint = true;

                // Setting pdf security if passwords are not null or empty
                if (!ownerPassword.IsNullOrEmpty() && !userPassword.IsNullOrEmpty())
                {
                    pdfSecuritySettings.OwnerPassword = ownerPassword!;
                    pdfSecuritySettings.UserPassword = userPassword!;
                }

                using MemoryStream pdfStream = new();
                pdfDocument.Save(stream: pdfStream, closeStream: false);
                pdfData = pdfStream.ToArray();

                pdfDocument.Close();
            }

            return pdfData;
        }

        /// <summary>
        /// Encrypts pdf from <see cref="MemoryStream"/> value
        /// </summary>
        /// <param name="data">Pdf data</param>
        /// <param name="ownerPassword">Owner password to set</param>
        /// <param name="userPassword">User password to set</param>
        /// <returns>New <see cref="byte"/> array from pdf data encrypted</returns>
        /// <remarks>If <paramref name="ownerPassword"/> and <paramref name="userPassword"/> are null or empty pdf file won't be secured</remarks>
        public static byte[] Encrypt(byte[]? data, string? ownerPassword, string? userPassword)
        {
            byte[] pdfData = [];

            if (!data.IsNullOrEmpty())
            {
                using MemoryStream openPdfStream = new(buffer: pdfData!);
                openPdfStream.Position = 0;

                using PdfDocument pdfDocument = PdfReader.Open(stream: openPdfStream);

                PdfSecuritySettings pdfSecuritySettings = pdfDocument.SecuritySettings;
                pdfSecuritySettings.PermitAnnotations = false;
                pdfSecuritySettings.PermitAssembleDocument = false;
                pdfSecuritySettings.PermitExtractContent = false;
                pdfSecuritySettings.PermitFormsFill = true;
                pdfSecuritySettings.PermitFullQualityPrint = true;
                pdfSecuritySettings.PermitModifyDocument = false;
                pdfSecuritySettings.PermitPrint = true;

                // Setting pdf security if passwords are not null or empty
                if (!ownerPassword.IsNullOrEmpty() && !userPassword.IsNullOrEmpty())
                {
                    pdfSecuritySettings.OwnerPassword = ownerPassword!;
                    pdfSecuritySettings.UserPassword = userPassword!;
                }

                using MemoryStream pdfStream = new();
                pdfDocument.Save(stream: pdfStream, closeStream: false);
                pdfData = pdfStream.ToArray();

                pdfDocument.Close();
            }

            return pdfData;
        }

    }
}
