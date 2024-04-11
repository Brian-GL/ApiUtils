using ClosedXML.Excel;
using System.Data;

namespace ApiUtils.Extensions
{
    /// <summary>
    /// <see cref="DataTable"/> extensions' functions class
    /// </summary>
    public static class DataTableExtensions
    {
        /// <summary>
        /// Exports basic excel file from <see cref="DataTable"/> <paramref name="value"/> data source
        /// </summary>
        /// <param name="value">Data source</param>
        /// <returns>Excel <see cref="byte"/> array data</returns>
        public static byte[] ExportExcel(this DataTable? value)
        {
            if (value.IsNullOrEmpty())
                return [];

            using XLWorkbook xLWorkbook = new();
            xLWorkbook.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            xLWorkbook.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
            xLWorkbook.CalculateMode = XLCalculateMode.Auto;
            xLWorkbook.Properties.Created = DateTime.Now;
            xLWorkbook.ShowGridLines = true;
            xLWorkbook.Worksheets.Add(dataTable: value).ColumnsUsed().AdjustToContents();

            using MemoryStream memoryStream = new();
            xLWorkbook.SaveAs(stream: memoryStream);
            byte[] data = memoryStream.ToArray();

            return data;
        }

        /// <summary>
        /// Defines if any <see cref="DataTable"/> is empty or null
        /// </summary>
        /// <param name="value">Value to validate if is empty or null</param>
        /// <returns><see cref="true"/> if <paramref name="value"/> is empty or null. <see cref="false"/> otherwise</returns>
        public static bool IsNullOrEmpty(this DataTable? value)
        {
            if (value is null)
                return true;

            return value!.Rows.Count == 0 || value.Columns.Count == 0;
        }
    }
}
