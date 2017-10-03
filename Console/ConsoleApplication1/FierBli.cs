using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace ConsoleApplication1
{
    /// <summary>
    /// NPOI操作Excel-多个表合并计算生成新表放到原来的工作簿里头
    /// </summary>
    public class FierBli
    {
        private IWorkbook _workbook;
        private string _filePath;

        public static List<string> SheetNames
        {
            get; set;
        }
        public FierBli(string path)
        {
            _filePath = path;
            SheetNames = new List<string>();
            LoadFile(_filePath);
        }

        private List<string> LoadFile(string filePath)
        {
            _filePath = filePath;
            SheetNames = new List<string>();
            using(var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                _workbook = WorkbookFactory.Create(fs);
            }
            return GetSheetNames();
        }

        private List<string> GetSheetNames()
        {
            var count = _workbook.NumberOfSheets;
            for(int i = 0;i < count;i++)
            {
                SheetNames.Add(_workbook.GetSheetName(i));
            }
            return SheetNames;
        }

        /// <summary>
		/// 获取所有数据，所有sheet的数据转化为datatable。
		/// </summary>
		/// <returns></returns>
		public DataSet GetAllTables()
        {
            var stopTime = new System.Diagnostics.Stopwatch();
            stopTime.Start();
            var ds = new DataSet();

            foreach(var sheetName in SheetNames)
            {
                ds.Tables.Add(ExcelToDataTable(sheetName, false));
            }
            stopTime.Stop();
            Console.WriteLine("GetData:" + stopTime.ElapsedMilliseconds / 1000 + "S");
            return ds;
        }

        private DataTable ExcelToDataTable(string sheetName, bool isFirstRowColumn)
        {
            ISheet sheet = null;
            var data = new DataTable();
            data.TableName = sheetName;
            int startRow = 0;
            try
            {
                sheet = sheetName != null ? _workbook.GetSheet(sheetName) : _workbook.GetSheetAt(0);
                if(sheet != null)
                {
                    var firstRow = sheet.GetRow(0);
                    if(firstRow == null)
                        return data;
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数
                    startRow = isFirstRowColumn ? sheet.FirstRowNum + 1 : sheet.FirstRowNum;

                    for(int i = firstRow.FirstCellNum;i < cellCount;++i)
                    {
                        //.StringCellValue;
                        var column = new DataColumn(Convert.ToChar(((int)'A') + i).ToString());
                        if(isFirstRowColumn)
                        {
                            var columnName = firstRow.GetCell(i).StringCellValue;
                            column = new DataColumn(columnName);
                        }
                        data.Columns.Add(column);
                    }


                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for(int i = startRow;i <= rowCount;++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if(row == null)
                            continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for(int j = row.FirstCellNum;j < cellCount;++j)
                        {
                            if(row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j, MissingCellPolicy.RETURN_NULL_AND_BLANK).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }
                else
                    throw new Exception("Don not have This Sheet");

                return data;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        public void AddNewSheet(string name)
        {
            ISheet sht = _workbook.CreateSheet(name + DateTime.Now.ToString("yyyyMMddHHmmss"));
            //新表用来存结果
            //然后把前面的数据拿来加
            DataSet ds = GetAllTables();
            IRow row_head = sht.CreateRow(0);
            int MaxColumntableIndex = GetMaxCindex(ds);

            for(int i = 0;i < ds.Tables[MaxColumntableIndex].Columns.Count;i++)
            {
                ICell c_head = row_head.CreateCell(i);
                c_head.SetCellValue(ds.Tables[MaxColumntableIndex].Columns[i].Caption);
            }

            ComputeAll(ds, MaxColumntableIndex, GetMaxRindex(ds), sht);

            using(FileStream fileStream = File.Open(_filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                _workbook.Write(fileStream);
                fileStream.Close();
            }
        }

        private void ComputeAll(DataSet ds, int max_rows_index, int max_column_index, ISheet sht)
        {
            for(int j = 1;j < ds.Tables[max_rows_index].Rows.Count;j++)//从第一行开始
            {
                IRow row = sht.CreateRow(j);
                for(int l = 0;l < ds.Tables[max_column_index].Columns.Count;l++)
                {
                    //构建大表完成
                    ICell cell = row.CreateCell(l);
                    double result = 0.00;
                    //把ds里同位置的加起来
                    foreach(DataTable dt in ds.Tables)
                    {
                        try
                        {
                            result += Convert.ToDouble(dt.Rows[j][l]);
                        }
                        catch
                        {
                            result += 0;
                        }
                    }
                    cell.SetCellValue(result);
                }
            }

        }

        private int GetMaxCindex(DataSet ds)
        {
            int max = 0;
            int cols = 0;
            for(int i = 0;i < ds.Tables.Count;i++)
            {
                if(ds.Tables[i].Columns.Count > cols)
                {
                    cols = ds.Tables[i].Columns.Count;
                    max = i;
                }
            }
            return max;
        }
        private int GetMaxRindex(DataSet ds)
        {
            int max = 0;
            int rows = 0;
            for(int i = 0;i < ds.Tables.Count;i++)
            {
                if(ds.Tables[i].Rows.Count > rows)
                {
                    rows = ds.Tables[i].Rows.Count;
                    max = i;
                }
            }
            return max;
        }
    }
}
