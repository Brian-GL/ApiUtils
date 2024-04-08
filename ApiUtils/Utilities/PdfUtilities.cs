using ApiUtils.Exceptions;
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
        /// Encrypts pdf from <see cref="MemoryStream"/>
        /// </summary>
        /// <param name="memoryStream">Pdf stream</param>
        /// <param name="ownerPassword">Owner password to set</param>
        /// <param name="userPassword">User password to set</param>
        /// <returns>New <see cref="byte"/> array from pdf data encrypted</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="memoryStream"/> is null</exception>
        /// <exception cref="ArgumentEmptyException">If <paramref name="ownerPassword"/> is null, empty or full with white spaces</exception>
        /// <exception cref="ArgumentEmptyException">If <paramref name="memoryStream"/> is null, empty or full with white spaces</exception>
        public static byte[] Encrypt(MemoryStream memoryStream, string? ownerPassword, string? userPassword)
        {
            ArgumentNullException.ThrowIfNull(argument: memoryStream, paramName: nameof(memoryStream));
            ArgumentEmptyException.ThrowIfEmpty(argument: ownerPassword, paramName: nameof(ownerPassword));
            ArgumentEmptyException.ThrowIfEmpty(argument: userPassword, paramName: nameof(userPassword));

            byte[] pdfData = [];

            using (PdfDocument pdfDocument = PdfReader.Open(stream: memoryStream))
            {
                PdfSecuritySettings pdfSecuritySettings = pdfDocument.SecuritySettings;
                pdfSecuritySettings.PermitAnnotations = false;
                pdfSecuritySettings.PermitAssembleDocument = false;
                pdfSecuritySettings.PermitExtractContent = false;
                pdfSecuritySettings.PermitFormsFill = true;
                pdfSecuritySettings.PermitFullQualityPrint = true;
                pdfSecuritySettings.PermitModifyDocument = false;
                pdfSecuritySettings.OwnerPassword = ownerPassword!;
                pdfSecuritySettings.PermitPrint = true;
                pdfSecuritySettings.UserPassword = userPassword!;

                using MemoryStream pdfStream = new();
                pdfDocument.Save(stream: pdfStream, closeStream: false);
                pdfData = pdfStream.ToArray();

                pdfDocument.Close();
            }

            return pdfData;
        }
    }
}
