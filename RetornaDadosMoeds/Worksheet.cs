using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Text;

namespace RetornaDadosMoeds
{
   public static class Worksheet 
    {
        public static void ReadCsvMoeda()
        {
            var path = @"D:\dowload\DadosMoeda.csv";
            using (StreamReader streamReader = new StreamReader(path))
            {
                var read = streamReader.ReadToEnd();

                Console.WriteLine(read);

                var listMoeda = new List<string>();
                foreach (var item in read)
                {
                    listMoeda.Add(item.ToString());
                }
            }
        }
        public static void ReadCsvCotacao()
        {
            var path = @"D:\dowload\DadosCotacao.csv";
            using (StreamReader streamReader = new StreamReader(path))
            {
                var read = streamReader.ReadToEnd();

                Console.WriteLine(read);
                var listMoeda = new List<string>();
                foreach (var item in read)
                {
                    listMoeda.Add(item.ToString());
                    
                }

                ReturnCsv(listMoeda);


            }
        }

        public static void ReturnCsv(List<string> listMoeda)
        {
            Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                Console.WriteLine("Excel is not properly installed!!");

            }
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlWorkBook = xlApp.Workbooks.Add(listMoeda);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

           
            xlWorkSheet.Columns[1,1] = "ID_MOEDA";
            xlWorkSheet.Columns[2,1] = "DATA_REF";
            xlWorkSheet.Columns[3, 1] = "VL_COTACAO";
            xlWorkSheet.Cells[1, 1] = "";
            xlWorkSheet.Cells[1, 2] = "Name";
            xlWorkSheet.Cells[2, 1] = "1";
            xlWorkSheet.Cells[2, 2] = "One";

            string newFileName = System.IO.Directory.GetCurrentDirectory() + "\\Resultado_aaaammdd_HHmmss.csv";

            // Now save this file as CSV file.
            xlWorkBook.SaveAs(newFileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlCSV);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

        }
    }
}
