using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using NPOI;
using NPOI.HPSF;
using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.POIFS;
using NPOI.SS.UserModel;
using NPOI.Util;
using NPOI.SS;
using NPOI.DDF;
using NPOI.SS.Util;
using System.Collections;
using System.Text.RegularExpressions;
using NPOI.SS.Formula.Eval;

namespace SucLib.Common
{
    public class NPOIHelper
    {
        #region

        protected static HSSFWorkbook hssfworkbook;

        protected static void InitializeWorkbook()
        {
            hssfworkbook = new HSSFWorkbook();

            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "成都联宇创新科技有限公司@easyman";
            hssfworkbook.DocumentSummaryInformation = dsi;

            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Author = "easyman";
            si.Comments = "luoxiang";
            si.Title = "指标得分模版";
            hssfworkbook.SummaryInformation = si;
        }

        /// <summary>
        /// 单表头
        /// </summary>
        /// <param name="dtSource"></param>
        /// <param name="dtHead"></param>
        /// <param name="strHeaderText"></param>
        /// <returns></returns>
        static MemoryStream ExportDT(DataTable dtSource, DataTable dtHead, string strHeaderText)
        {
            InitializeWorkbook();
            ISheet sheet = hssfworkbook.CreateSheet(strHeaderText);

            //----------------表头样式
            HSSFCellStyle headStyle = getheadStyle();
            //----------------

            HSSFRow headerRow = sheet.CreateRow(0) as HSSFRow;
            headerRow.HeightInPoints = 30;

            int _ColWidth = 100; // 默认列宽
            for (int i = 0; i < dtHead.Rows.Count; i++)
            {
                object _name = dtHead.Rows[i]["title"];
                object _width = dtHead.Rows[i]["width"];

                if (_width != null)
                    _ColWidth = (Convert.ToInt32(_width) * 80);
                else
                    _ColWidth = (_ColWidth * 80);

                headerRow.CreateCell(i).SetCellValue(_name.ToString());
                headerRow.GetCell(i).CellStyle = headStyle;
                //设置列宽
                sheet.SetColumnWidth(i, _ColWidth);
            }
            //------------------------------以上为表头
            int rowIndex = 1;
            foreach (DataRow row in dtSource.Rows)
            {
                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = hssfworkbook.CreateSheet(strHeaderText);
                        rowIndex = 0;
                    }
                }

                HSSFRow dataRow = sheet.CreateRow(rowIndex) as HSSFRow;

                for (int m = 0; m < dtHead.Rows.Count; m++) //列
                {
                    HSSFCell newCell = dataRow.CreateCell(m) as HSSFCell;

                    object field = dtHead.Rows[m]["field"];
                    if (field != null)
                    {
                        string drValue = row[field.ToString()].ToString();
                        newCell.SetCellValue(drValue);
                    }
                }

                rowIndex++;
            }
            using (MemoryStream ms = new MemoryStream())
            {
                hssfworkbook.Write(ms);
                ms.Flush();
                ms.Position = 0;

                sheet = null;
                hssfworkbook = null;
                return ms;
            }
        }

        static HSSFCellStyle getBodyStyle()
        {
            ICellStyle cellStyle = hssfworkbook.CreateCellStyle();
            cellStyle.Alignment = HorizontalAlignment.Left;
            //---------设置字体
            IFont font = hssfworkbook.CreateFont();
            font.FontHeightInPoints = 10;
            font.FontName = "宋体";
            cellStyle.SetFont(font);
            //----------设置边框
            cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle.IsLocked = false;
            return (HSSFCellStyle)cellStyle;
        }

        static HSSFCellStyle getheadStyle()
        {
            ICellStyle cellStyle = hssfworkbook.CreateCellStyle();
            cellStyle.Alignment = HorizontalAlignment.Center;
            //---------设置字体
            IFont font = hssfworkbook.CreateFont();
            font.FontHeightInPoints = 10;
            font.Boldweight = 700;
            font.FontName = "宋体";
            cellStyle.SetFont(font);
            //----------设置边框
            cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            //----------设置背景颜色
            cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightCornflowerBlue.Index;
            cellStyle.FillPattern = FillPattern.AltBars;
            cellStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.LightCornflowerBlue.Index;
            cellStyle.IsLocked = true;
            return (HSSFCellStyle)cellStyle;
        }

        /// <summary>
        /// 复杂表头
        /// </summary>
        /// <param name="dtSource"></param>
        /// <param name="dtcolumns"></param>
        /// <param name="dtMerger"></param>
        /// <param name="strHeaderText"></param>
        /// <returns></returns>
        static MemoryStream ExportDT(DataTable dtSource, DataTable dtcolumns, DataTable dtMerger, string strHeaderText)
        {
            InitializeWorkbook();
            ISheet sheet = hssfworkbook.CreateSheet(strHeaderText);

            //----------------样式
            HSSFCellStyle headStyle = getheadStyle();
            HSSFCellStyle BodyStyle = getBodyStyle();

            //----------------

            HSSFRow headerRow = sheet.CreateRow(0) as HSSFRow;
            headerRow.HeightInPoints = 20;//第一行

            HSSFRow SecondRow = sheet.CreateRow(1) as HSSFRow;
            SecondRow.HeightInPoints = 20;//第二行

            int _ColWidth = 100; // 默认列宽

            int col2 = 0;
            for (int col = 0; col < dtcolumns.Rows.Count; col++) //第一行
            {
                object _rowspan = dtcolumns.Rows[col]["rowspan"];//合并行数
                if (_rowspan.ToString() == "2")
                {
                    col2++;
                }
            }

            int m = 0;
            int rIndex = 1;
            for (int i = 0; i < dtcolumns.Rows.Count; i++) //第一行
            {
                object _name = dtcolumns.Rows[i]["title"];
                object _width = dtcolumns.Rows[i]["width"];
                object _rowspan = dtcolumns.Rows[i]["rowspan"];//合并行数
                object _colspan = dtcolumns.Rows[i]["colspan"];//合并列数

                if (_rowspan.ToString() == "2")
                {
                    headerRow.CreateCell(i).SetCellValue(_name.ToString());
                    headerRow.GetCell(i).CellStyle = headStyle;
                    _ColWidth = (Convert.ToInt32(_width) * 60);
                    //设置列宽
                    sheet.SetColumnWidth(i, _ColWidth);

                    SecondRow.CreateCell(i).SetCellValue("");
                    SecondRow.GetCell(i).CellStyle = headStyle;

                    sheet.AddMergedRegion(new CellRangeAddress(0, 1, i, i));
                }
                else if (_colspan.ToString() == "6")
                {
                    m = col2 + rIndex * 6;
                    headerRow.CreateCell(m - 6).SetCellValue(_name.ToString());
                    headerRow.GetCell(m - 6).CellStyle = headStyle;

                    for (int j = 1; j < 7; j++) //第二行
                    {
                        string name = "";
                        if (j == 1)
                            name = "进度得分";
                        else if (j == 2)
                            name = "日完成值";
                        else if (j == 3)
                            name = "日目标值 ";
                        else if (j == 4)
                            name = "完成率";
                        else if (j == 5)
                            name = "月完成值";
                        else if (j == 6)
                            name = "月目标值";




                        SecondRow.CreateCell(m - j).SetCellValue(name.ToString());
                        SecondRow.GetCell(m - j).CellStyle = headStyle;

                        _ColWidth = (60 * 60);
                        //设置列宽
                        sheet.SetColumnWidth(m - j, _ColWidth);
                    }
                    sheet.AddMergedRegion(new CellRangeAddress(0, 0, m - 6, m - 1));
                    rIndex++;
                }
            }

            //------------------------------以上为表头
            int rowIndex = 2;
            foreach (DataRow row in dtSource.Rows)
            {
                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = hssfworkbook.CreateSheet(strHeaderText);
                        rowIndex = 0;
                    }
                }

                HSSFRow dataRow = sheet.CreateRow(rowIndex) as HSSFRow;
                foreach (DataColumn column in dtSource.Columns)
                {
                    if (column.Ordinal != dtSource.Columns.Count - 1)
                    {
                        HSSFCell newCell = dataRow.CreateCell(column.Ordinal) as HSSFCell;
                        if (column.ColumnName.Contains("_RATE")) //计算完成率
                        {
                            string sValue = row[column.Ordinal - 1].ToString().Trim();//实际值
                            string fValue = row[column.Ordinal - 2].ToString().Trim();//目标值
                            string wValue = "";
                            if (!string.IsNullOrEmpty(sValue) && !string.IsNullOrEmpty(fValue))
                            {
                                if (sValue != "--" && fValue != "--")
                                {
                                    if (sValue.Contains("%") || fValue.Contains("%"))
                                    {
                                        sValue = sValue.Replace("%", "");
                                        fValue = fValue.Replace("%", "");
                                    }
                                    try
                                    {
                                        decimal temp;
                                        if (decimal.TryParse(fValue, out temp))
                                        {
                                            decimal _fValue = temp;
                                            if (_fValue != 0)
                                                wValue = ((Convert.ToDecimal(sValue) / Convert.ToDecimal(_fValue)) * 100).ToString("F2") + "%";
                                            else
                                                wValue = "--";
                                        }
                                        else
                                        {
                                            wValue = "--";
                                        }
                                    }
                                    catch
                                    {
                                        wValue = "--";
                                    }
                                    newCell.SetCellValue(wValue);
                                    newCell.CellStyle = BodyStyle;
                                }
                                else
                                {
                                    newCell.SetCellValue("--");
                                    newCell.CellStyle = BodyStyle;
                                }
                            }
                        }
                        else
                        {
                            string drValue = row[column].ToString();
                            newCell.SetCellValue(drValue);
                            newCell.CellStyle = BodyStyle;
                        }
                    }
                }

                rowIndex++;
            }

            using (MemoryStream ms = new MemoryStream())
            {
                hssfworkbook.Write(ms);
                ms.Flush();
                ms.Position = 0;

                sheet = null;
                hssfworkbook = null;
                return ms;
            }
        }

        /// <summary>
        ///  DataTable导出到Excel文件,单表头
        /// </summary>
        /// <param name="dtSource">数据表格</param>
        /// <param name="dtHead">表头表格</param>
        /// <param name="strHeaderText">sheet名称</param>
        /// <param name="strFileName">文件名称</param>
        /// <param name="_rowsCount">表头行数,从1开始</param>
        public static void ExportDTtoExcel(DataTable dtSource, DataTable dtHead, string strHeaderText, string strFileName)
        {
            using (MemoryStream ms = ExportDT(dtSource, dtHead, strHeaderText))
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }

        /// <summary>
        /// 复杂表头导出
        /// </summary>
        /// <param name="dtSource">数据表格</param>
        /// <param name="dtcolumns">合并行</param>
        /// <param name="dtMerger">合并列</param>
        /// <param name="strHeaderText">sheet名称</param>
        /// <param name="strFileName">文件名称</param>
        public static void ExportDTtoExcel(DataTable dtSource, DataTable dtcolumns, DataTable dtMerger, string strHeaderText, string strFileName)
        {
            using (MemoryStream ms = ExportDT(dtSource, dtcolumns, dtMerger, strHeaderText))
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }

        #endregion

        #region 从excel中将数据导出到datatable
        /// <summary>读取excel
        /// 默认第一行为标头
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <returns></returns>
        public static DataTable ImportExceltoDt(string strFileName)
        {
            DataTable dt = new DataTable();
            HSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);
            }
            HSSFSheet sheet = hssfworkbook.GetSheetAt(0) as HSSFSheet;
            dt = ImportDt(sheet, 0, true);
            return dt;
        }

        /// <summary>
        /// 读取excel
        /// </summary>
        /// <param name="strFileName">excel文件路径</param>
        /// <param name="sheet">需要导出的sheet</param>
        /// <param name="HeaderRowIndex">列头所在行号，-1表示没有列头</param>
        /// <returns></returns>
        public static DataTable ImportExceltoDt(string strFileName, string SheetName, int HeaderRowIndex)
        {
            HSSFWorkbook workbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                workbook = new HSSFWorkbook(file);
            }
            HSSFSheet sheet = workbook.GetSheet(SheetName) as HSSFSheet;
            DataTable table = new DataTable();
            table = ImportDt(sheet, HeaderRowIndex, true);
            //ExcelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }

        /// <summary>
        /// 读取excel
        /// </summary>
        /// <param name="strFileName">excel文件路径</param>
        /// <param name="sheet">需要导出的sheet序号</param>
        /// <param name="HeaderRowIndex">列头所在行号，-1表示没有列头</param>
        /// <returns></returns>
        public static DataTable ImportExceltoDt(string strFileName, int SheetIndex, int HeaderRowIndex)
        {
            HSSFWorkbook workbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                workbook = new HSSFWorkbook(file);
            }
            HSSFSheet sheet = workbook.GetSheetAt(SheetIndex) as HSSFSheet;
            DataTable table = new DataTable();
            table = ImportDt(sheet, HeaderRowIndex, true);
            //ExcelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }

        /// <summary>
        /// 读取excel
        /// </summary>
        /// <param name="strFileName">excel文件路径</param>
        /// <param name="sheet">需要导出的sheet</param>
        /// <param name="HeaderRowIndex">列头所在行号，-1表示没有列头</param>
        /// <returns></returns>
        public static DataTable ImportExceltoDt(string strFileName, string SheetName, int HeaderRowIndex, bool needHeader)
        {
            HSSFWorkbook workbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                workbook = new HSSFWorkbook(file);
            }
            HSSFSheet sheet = workbook.GetSheet(SheetName) as HSSFSheet;
            DataTable table = new DataTable();
            table = ImportDt(sheet, HeaderRowIndex, needHeader);
            //ExcelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }

        /// <summary>
        /// 读取excel
        /// </summary>
        /// <param name="strFileName">excel文件路径</param>
        /// <param name="sheet">需要导出的sheet序号</param>
        /// <param name="HeaderRowIndex">列头所在行号，-1表示没有列头</param>
        /// <returns></returns>
        public static DataTable ImportExceltoDt(string strFileName, int SheetIndex, int HeaderRowIndex, bool needHeader)
        {
            HSSFWorkbook workbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                workbook = new HSSFWorkbook(file);
            }
            HSSFSheet sheet = workbook.GetSheetAt(SheetIndex) as HSSFSheet;
            DataTable table = new DataTable();
            table = ImportDt(sheet, HeaderRowIndex, needHeader);
            //ExcelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }

        /// <summary>
        /// 将制定sheet中的数据导出到datatable中
        /// </summary>
        /// <param name="sheet">需要导出的sheet</param>
        /// <param name="HeaderRowIndex">列头所在行号，-1表示没有列头</param>
        /// <returns></returns>
        static DataTable ImportDt(HSSFSheet sheet, int HeaderRowIndex, bool needHeader)
        {
            DataTable table = new DataTable();
            HSSFRow headerRow;
            int cellCount;
            try
            {
                if (HeaderRowIndex < 0 || !needHeader)
                {
                    headerRow = sheet.GetRow(0) as HSSFRow;
                    cellCount = headerRow.LastCellNum;

                    for (int i = headerRow.FirstCellNum; i <= cellCount; i++)
                    {
                        DataColumn column = new DataColumn(Convert.ToString(i));
                        table.Columns.Add(column);
                    }
                }
                else
                {
                    headerRow = sheet.GetRow(HeaderRowIndex) as HSSFRow;
                    cellCount = headerRow.LastCellNum;

                    for (int i = headerRow.FirstCellNum; i <= cellCount; i++)
                    {
                        if (headerRow.GetCell(i) == null)
                        {
                            if (table.Columns.IndexOf(Convert.ToString(i)) > 0)
                            {
                                DataColumn column = new DataColumn(Convert.ToString("重复列名" + i));
                                table.Columns.Add(column);
                            }
                            else
                            {
                                DataColumn column = new DataColumn(Convert.ToString(i));
                                table.Columns.Add(column);
                            }

                        }
                        else if (table.Columns.IndexOf(headerRow.GetCell(i).ToString()) > 0)
                        {
                            DataColumn column = new DataColumn(Convert.ToString("重复列名" + i));
                            table.Columns.Add(column);
                        }
                        else
                        {
                            DataColumn column = new DataColumn(headerRow.GetCell(i).ToString());
                            table.Columns.Add(column);
                        }
                    }
                }
                int rowCount = sheet.LastRowNum;
                for (int i = (HeaderRowIndex + 1); i <= sheet.LastRowNum; i++)
                {
                    try
                    {
                        HSSFRow row;
                        if (sheet.GetRow(i) == null)
                        {
                            row = sheet.CreateRow(i) as HSSFRow;
                        }
                        else
                        {
                            row = sheet.GetRow(i) as HSSFRow;
                        }

                        DataRow dataRow = table.NewRow();

                        for (int j = row.FirstCellNum; j <= cellCount; j++)
                        {
                            try
                            {
                                if (row.GetCell(j) != null)
                                {
                                    switch (row.GetCell(j).CellType)
                                    {
                                        case CellType.String:
                                            string str = row.GetCell(j).StringCellValue;
                                            if (str != null && str.Length > 0)
                                            {
                                                dataRow[j] = str.ToString();
                                            }
                                            else
                                            {
                                                dataRow[j] = null;
                                            }
                                            break;
                                        case CellType.Numeric:
                                            if (DateUtil.IsCellDateFormatted(row.GetCell(j)))
                                            {
                                                dataRow[j] = DateTime.FromOADate(row.GetCell(j).NumericCellValue);
                                            }
                                            else
                                            {
                                                dataRow[j] = Convert.ToDouble(row.GetCell(j).NumericCellValue);
                                            }
                                            break;
                                        case CellType.Boolean:
                                            dataRow[j] = Convert.ToString(row.GetCell(j).BooleanCellValue);
                                            break;
                                        case CellType.Error:
                                            dataRow[j] = ErrorEval.GetText(row.GetCell(j).ErrorCellValue);
                                            break;
                                        case CellType.Formula:
                                            switch (row.GetCell(j).CachedFormulaResultType)
                                            {
                                                case CellType.String:
                                                    string strFORMULA = row.GetCell(j).StringCellValue;
                                                    if (strFORMULA != null && strFORMULA.Length > 0)
                                                    {
                                                        dataRow[j] = strFORMULA.ToString();
                                                    }
                                                    else
                                                    {
                                                        dataRow[j] = null;
                                                    }
                                                    break;
                                                case CellType.Numeric:
                                                    dataRow[j] = Convert.ToString(row.GetCell(j).NumericCellValue);
                                                    break;
                                                case CellType.Boolean:
                                                    dataRow[j] = Convert.ToString(row.GetCell(j).BooleanCellValue);
                                                    break;
                                                case CellType.Error:
                                                    dataRow[j] = ErrorEval.GetText(row.GetCell(j).ErrorCellValue);
                                                    break;
                                                default:
                                                    dataRow[j] = "";
                                                    break;
                                            }
                                            break;
                                        default:
                                            dataRow[j] = "";
                                            break;
                                    }
                                }
                            }
                            catch (Exception exception)
                            {
                                throw exception;
                            }
                        }
                        table.Rows.Add(dataRow);
                    }
                    catch (Exception exception)
                    {
                        throw exception;
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table;
        }
        #endregion

        #region 更新excel中的数据
        /// <summary>
        /// 更新Excel表格
        /// </summary>
        /// <param name="outputFile">需更新的excel表格路径</param>
        /// <param name="sheetname">sheet名</param>
        /// <param name="updateData">需更新的数据</param>
        /// <param name="coluid">需更新的列号</param>
        /// <param name="rowid">需更新的开始行号</param>
        public static void UpdateExcel(string outputFile, string sheetname, string[] updateData, int coluid, int rowid)
        {
            FileStream readfile = new FileStream(outputFile, FileMode.Open, FileAccess.Read);

            HSSFWorkbook hssfworkbook = new HSSFWorkbook(readfile);
            ISheet sheet1 = hssfworkbook.GetSheet(sheetname);
            for (int i = 0; i < updateData.Length; i++)
            {
                try
                {
                    if (sheet1.GetRow(i + rowid) == null)
                    {
                        sheet1.CreateRow(i + rowid);
                    }
                    if (sheet1.GetRow(i + rowid).GetCell(coluid) == null)
                    {
                        sheet1.GetRow(i + rowid).CreateCell(coluid);
                    }

                    sheet1.GetRow(i + rowid).GetCell(coluid).SetCellValue(updateData[i]);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            try
            {
                readfile.Close();
                FileStream writefile = new FileStream(outputFile, FileMode.Create, FileAccess.Write);
                hssfworkbook.Write(writefile);
                writefile.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 更新Excel表格
        /// </summary>
        /// <param name="outputFile">需更新的excel表格路径</param>
        /// <param name="sheetname">sheet名</param>
        /// <param name="updateData">需更新的数据</param>
        /// <param name="coluids">需更新的列号</param>
        /// <param name="rowid">需更新的开始行号</param>
        public static void UpdateExcel(string outputFile, string sheetname, string[][] updateData, int[] coluids, int rowid)
        {
            FileStream readfile = new FileStream(outputFile, FileMode.Open, FileAccess.Read);

            HSSFWorkbook hssfworkbook = new HSSFWorkbook(readfile);
            readfile.Close();
            ISheet sheet1 = hssfworkbook.GetSheet(sheetname);
            for (int j = 0; j < coluids.Length; j++)
            {
                for (int i = 0; i < updateData[j].Length; i++)
                {
                    try
                    {
                        if (sheet1.GetRow(i + rowid) == null)
                        {
                            sheet1.CreateRow(i + rowid);
                        }
                        if (sheet1.GetRow(i + rowid).GetCell(coluids[j]) == null)
                        {
                            sheet1.GetRow(i + rowid).CreateCell(coluids[j]);
                        }
                        sheet1.GetRow(i + rowid).GetCell(coluids[j]).SetCellValue(updateData[j][i]);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            try
            {
                FileStream writefile = new FileStream(outputFile, FileMode.Create);
                hssfworkbook.Write(writefile);
                writefile.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 更新Excel表格
        /// </summary>
        /// <param name="outputFile">需更新的excel表格路径</param>
        /// <param name="sheetname">sheet名</param>
        /// <param name="updateData">需更新的数据</param>
        /// <param name="coluid">需更新的列号</param>
        /// <param name="rowid">需更新的开始行号</param>
        public static void UpdateExcel(string outputFile, string sheetname, double[] updateData, int coluid, int rowid)
        {
            FileStream readfile = new FileStream(outputFile, FileMode.Open, FileAccess.Read);

            HSSFWorkbook hssfworkbook = new HSSFWorkbook(readfile);
            ISheet sheet1 = hssfworkbook.GetSheet(sheetname);
            for (int i = 0; i < updateData.Length; i++)
            {
                try
                {
                    if (sheet1.GetRow(i + rowid) == null)
                    {
                        sheet1.CreateRow(i + rowid);
                    }
                    if (sheet1.GetRow(i + rowid).GetCell(coluid) == null)
                    {
                        sheet1.GetRow(i + rowid).CreateCell(coluid);
                    }

                    sheet1.GetRow(i + rowid).GetCell(coluid).SetCellValue(updateData[i]);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            try
            {
                readfile.Close();
                FileStream writefile = new FileStream(outputFile, FileMode.Create, FileAccess.Write);
                hssfworkbook.Write(writefile);
                writefile.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 更新Excel表格
        /// </summary>
        /// <param name="outputFile">需更新的excel表格路径</param>
        /// <param name="sheetname">sheet名</param>
        /// <param name="updateData">需更新的数据</param>
        /// <param name="coluids">需更新的列号</param>
        /// <param name="rowid">需更新的开始行号</param>
        public static void UpdateExcel(string outputFile, string sheetname, double[][] updateData, int[] coluids, int rowid)
        {
            FileStream readfile = new FileStream(outputFile, FileMode.Open, FileAccess.Read);

            HSSFWorkbook hssfworkbook = new HSSFWorkbook(readfile);
            readfile.Close();
            ISheet sheet1 = hssfworkbook.GetSheet(sheetname);
            for (int j = 0; j < coluids.Length; j++)
            {
                for (int i = 0; i < updateData[j].Length; i++)
                {
                    try
                    {
                        if (sheet1.GetRow(i + rowid) == null)
                        {
                            sheet1.CreateRow(i + rowid);
                        }
                        if (sheet1.GetRow(i + rowid).GetCell(coluids[j]) == null)
                        {
                            sheet1.GetRow(i + rowid).CreateCell(coluids[j]);
                        }
                        sheet1.GetRow(i + rowid).GetCell(coluids[j]).SetCellValue(updateData[j][i]);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            try
            {
                FileStream writefile = new FileStream(outputFile, FileMode.Create);
                hssfworkbook.Write(writefile);
                writefile.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public static int GetSheetNumber(string outputFile)
        {
            int number = 0;
            try
            {
                FileStream readfile = new FileStream(outputFile, FileMode.Open, FileAccess.Read);

                HSSFWorkbook hssfworkbook = new HSSFWorkbook(readfile);
                number = hssfworkbook.NumberOfSheets;

            }
            catch (Exception exception)
            {
                throw exception;
            }
            return number;
        }

        public static ArrayList GetSheetName(string outputFile)
        {
            ArrayList arrayList = new ArrayList();
            try
            {
                FileStream readfile = new FileStream(outputFile, FileMode.Open, FileAccess.Read);

                HSSFWorkbook hssfworkbook = new HSSFWorkbook(readfile);
                for (int i = 0; i < hssfworkbook.NumberOfSheets; i++)
                {
                    arrayList.Add(hssfworkbook.GetSheetName(i));
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return arrayList;
        }

        public static bool isNumeric(String message, out double result)
        {
            Regex rex = new Regex(@"^[-]?\d+[.]?\d*$");
            result = -1;
            if (rex.IsMatch(message))
            {
                result = double.Parse(message);
                return true;
            }
            else
                return false;

        }
        /// <summary>
        /// DataTable导出到Excel文件
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <param name="strFileName">保存位置</param>
        public void Export(DataTable dtSource, string strHeaderText, string strFileName)
        {
            using (MemoryStream ms = Export(dtSource, strHeaderText, false))
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }

        /// <summary>
        /// DataTable导出到Excel的MemoryStream
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <param name="headerIsShow">是否导出表头</param>
        public MemoryStream Export(DataTable dtSource, string strHeaderText, bool headerIsShow)
        {
            //IWorkbook workbook = new HSSFWorkbook();
            HSSFWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet();

            #region 右击文件 属性信息
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "绵阳移动";
                //workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "绵阳移动"; //填加xls文件作者信息
                si.ApplicationName = "绵阳移动"; //填加xls文件创建程序信息
                si.LastAuthor = "绵阳移动"; //填加xls文件最后保存者信息
                si.Comments = "绵阳移动"; //填加xls文件作者信息
                si.Title = "绵阳移动"; //填加xls文件标题信息
                si.Subject = "绵阳移动";//填加文件主题信息
                si.CreateDateTime = DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            ICellStyle dateStyle = workbook.CreateCellStyle();
            IDataFormat format = workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            //取得列宽
            int[] arrColWidth = new int[dtSource.Columns.Count];
            foreach (DataColumn item in dtSource.Columns)
            {
                arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
            }
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }
            int rowIndex = 0;
            foreach (DataRow row in dtSource.Rows)
            {
                #region 新建表，填充表头，填充列头，样式
                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = workbook.CreateSheet();
                    }

                    if (!headerIsShow)
                    {
                        #region 表头及样式
                        {
                            IRow headerRow = sheet.CreateRow(0);
                            headerRow.HeightInPoints = 25;
                            headerRow.CreateCell(0).SetCellValue(strHeaderText);

                            ICellStyle headStyle = workbook.CreateCellStyle();
                            headStyle.Alignment = HorizontalAlignment.Center;
                            headStyle.FillBackgroundColor = HSSFColor.Blue.Index;
                            headStyle.FillForegroundColor = HSSFColor.White.Index;
                            IFont font = workbook.CreateFont();
                            font.FontHeightInPoints = 15;
                            font.Boldweight = 500;
                            font.Color = HSSFColor.Blue.Index;
                            headStyle.SetFont(font);
                            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, dtSource.Columns.Count - 1));
                            headerRow.GetCell(0).CellStyle = headStyle;
                            headerRow = null;
                        }
                        #endregion
                    }


                    #region 列头及样式
                    {
                        IRow headerRow = sheet.CreateRow(headerIsShow ? 0 : 1);
                        ICellStyle headStyle = workbook.CreateCellStyle();
                        headStyle.Alignment = HorizontalAlignment.Center;
                        IFont font = workbook.CreateFont();
                        font.FontHeightInPoints = 10;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);
                        foreach (DataColumn column in dtSource.Columns)
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                            headerRow.GetCell(column.Ordinal).CellStyle = headStyle;

                            //设置列宽
                            sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);
                        }
                        headerRow = null;
                    }
                    #endregion

                    rowIndex = headerIsShow ? 1 : 2;
                }
                #endregion


                #region 填充内容
                IRow dataRow = sheet.CreateRow(rowIndex);
                foreach (DataColumn column in dtSource.Columns)
                {
                    ICell newCell = dataRow.CreateCell(column.Ordinal);
                    string drValue = row[column].ToString();
                    this.SetCellValue(newCell, dateStyle, column.DataType.ToString(), drValue);
                }
                #endregion

                rowIndex++;
            }
            MemoryStream ms = new MemoryStream();
            //using (MemoryStream ms = new MemoryStream())
            //{
            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;

            sheet = null;
            return ms;
            //}
        }

        /// <summary>
        /// 用于Web导出
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <param name="strFileName">文件名</param>
        public void ExportByWeb(DataTable dtSource, string strHeaderText, string strFileName)
        {
            HttpContext curContext = HttpContext.Current;

            // 设置编码和附件格式
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + HttpUtility.UrlEncode(strFileName, Encoding.UTF8));

            curContext.Response.BinaryWrite(Export(dtSource, strHeaderText, true).GetBuffer());
            curContext.Response.End();
        }

        /// <summary>读取excel
        /// 默认第一行为标头
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <returns></returns>
        public DataTable Import(string strFileName)
        {
            DataTable dt = new DataTable();

            HSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);
            }
            ISheet sheet = hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            IRow headerRow = sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;

            for (int j = 0; j < cellCount; j++)
            {
                ICell cell = headerRow.GetCell(j);
                dt.Columns.Add(cell.ToString());
            }

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                DataRow dataRow = dt.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }

                dt.Rows.Add(dataRow);
            }
            return dt;
        }

        /// <summary>读取excel
        /// 默认第一行为标头
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <returns></returns>
        public DataTable Import(string strFileName, int index)
        {
            DataTable dt = new DataTable();

            HSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);
            }
            ISheet sheet = hssfworkbook.GetSheetAt(index);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            IRow headerRow = sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;

            for (int j = 0; j < cellCount; j++)
            {
                ICell cell = headerRow.GetCell(j);
                dt.Columns.Add(cell.ToString());
            }

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                DataRow dataRow = dt.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                    {
                        if (row.GetCell(j).CellType == CellType.Numeric && HSSFDateUtil.IsCellDateFormatted(row.GetCell(j)))
                        {
                            dataRow[j] = row.GetCell(j).DateCellValue.ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            dataRow[j] = row.GetCell(j).ToString();
                        }
                    }

                }

                dt.Rows.Add(dataRow);
            }
            return dt;
        }

        /// <summary>
        /// 设置单元格值
        /// </summary>
        private void SetCellValue(ICell cell, ICellStyle dateStyle, string type, string drValue)
        {
            switch (type)
            {
                case "System.String"://字符串类型
                    cell.SetCellValue(drValue);
                    break;
                case "System.DateTime"://日期类型
                    DateTime dateV;
                    DateTime.TryParse(drValue, out dateV);
                    cell.SetCellValue(dateV);

                    cell.CellStyle = dateStyle;//格式化显示
                    break;
                case "System.Boolean"://布尔型
                    bool boolV = false;
                    bool.TryParse(drValue, out boolV);
                    cell.SetCellValue(boolV);
                    break;
                case "System.Int16"://整型
                case "System.Int32":
                case "System.Int64":
                case "System.Byte":
                    int intV = 0;
                    int.TryParse(drValue, out intV);
                    cell.SetCellValue(intV);
                    break;
                case "System.Decimal"://浮点型
                case "System.Double":
                    double doubV = 0;
                    double.TryParse(drValue, out doubV);
                    cell.SetCellValue(doubV);
                    break;
                case "System.DBNull"://空值处理
                    cell.SetCellValue("");
                    break;
                default:
                    cell.SetCellValue("");
                    break;
            }
        }

        public string GetCellString(IRow row, int cellNum, bool AllowNull)
        {
            if (row.GetCell(cellNum) == null)
            {
                if (AllowNull)
                {
                    return string.Empty;
                }
                else
                {
                    throw new ArgumentNullException((cellNum + 1).ToString() + 1);
                }
            }
            if (string.IsNullOrEmpty(row.GetCell(cellNum).ToString().Trim()))
            {
                if (AllowNull)
                {
                    return string.Empty;
                }
                else
                {
                    throw new ArgumentNullException((cellNum + 1).ToString());
                }
            }
            if (row.GetCell(cellNum).CellType == CellType.Numeric)
            {
                //NPOI中数字和日期都是NUMERIC类型的，这里对其进行判断是否是日期类型
                if (HSSFDateUtil.IsCellDateFormatted(row.GetCell(cellNum)))//日期类型
                {
                    return row.GetCell(cellNum).DateCellValue.ToString("yyyy-MM-dd");
                }
            }
            return row.GetCell(cellNum).ToString().Trim();
        }
    }
}
