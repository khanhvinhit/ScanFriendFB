using System;

using System.Data;

namespace ScanFriendFB
{
    public class ExportToExcel
    {
        private static ExportToExcel _instance;

        public static ExportToExcel Instance
        {
            get
            {
                if (_instance == null) _instance = new ExportToExcel();
                return _instance;
            }
            private set { _instance = value; }
        }

        public void Export(DataTable dt, string sheetName, string title)
        {
            Microsoft.Office.Interop.Excel.Application oExcel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbooks oBooks;
            Microsoft.Office.Interop.Excel.Sheets oSheets;
            Microsoft.Office.Interop.Excel.Workbook oBook;
            Microsoft.Office.Interop.Excel.Worksheet oSheet;
            oExcel.Visible = true;
            oExcel.DisplayAlerts = false;
            oExcel.Application.SheetsInNewWorkbook = 1;
            oBooks = oExcel.Workbooks;
            oBook = (Microsoft.Office.Interop.Excel.Workbook)(oExcel.Workbooks.Add(Type.Missing));
            oSheets = oBook.Worksheets;
            oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oSheets.get_Item(1);
            oSheet.Name = sheetName;
            Microsoft.Office.Interop.Excel.Range head = oSheet.get_Range("C1", "H1");
            head.MergeCells = true;
            head.Value2 = title;
            head.Font.Bold = true;
            head.Font.Name = "Tahoma";
            head.Font.Size = "18";
            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            for (int i = 1; i <= dt.Columns.Count; i++)
            {
                string[] arr1 = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };//
                string name = arr1[i - 1] + 3;
                oSheet.get_Range(arr1[i - 1] + 3, arr1[dt.Columns.Count - 1] + 3).Font.Bold = true;
                oSheet.get_Range(arr1[i - 1] + 3, arr1[dt.Columns.Count - 1] + 3).Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
                oSheet.get_Range(arr1[i - 1] + 3, arr1[dt.Columns.Count - 1] + 3).Interior.ColorIndex = 15;
                oSheet.get_Range(arr1[i - 1] + 3, arr1[dt.Columns.Count - 1] + 3).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                oSheet.get_Range(name, name);
                oSheet.Range[name, name].Value2 = dt.Columns[i - 1].ColumnName;
                oSheet.Range[name, name].ColumnWidth = 15.0;
            }
            object[,] arr = new object[dt.Rows.Count, dt.Columns.Count];
            for (int r = 0; r < dt.Rows.Count; r++)
            {
                DataRow dr = dt.Rows[r];
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    arr[r, c] = dr[c].ToString();
                }
            }
            int rowStart = 4;
            int columnStart = 1;
            int rowEnd = rowStart + dt.Rows.Count - 1;
            int columnEnd = dt.Columns.Count;
            Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowStart, columnStart];
            Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnEnd];
            Microsoft.Office.Interop.Excel.Range range = oSheet.get_Range(c1, c2);
            range.Value2 = arr;
            range.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
            Microsoft.Office.Interop.Excel.Range c3 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnStart];
            Microsoft.Office.Interop.Excel.Range c4 = oSheet.get_Range(c1, c3);
            oSheet.get_Range(c3, c4).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
        }
    }
}