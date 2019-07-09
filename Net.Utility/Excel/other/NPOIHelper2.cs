using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using NPOI;
using NPOI.HPSF;
using NPOI.HSSF;
using NPOI.SS.UserModel;
using NPOI.POIFS;
using NPOI.Util;
using NPOI.HSSF.UserModel;
using System.Text;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;

/// <summary>
/// 
/// </summary>
public class NPOIHelper
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dtSource"></param>
    /// <param name="strHeaderText"></param>
    /// <param name="strFileName"></param>
    public static void Export(DataTable dtSource, string strHeaderText, string strFileName)
    {
        using (MemoryStream ms = Export(dtSource, strHeaderText))
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
    /// 从Excel中获取数据到DataTable  
    /// </summary>  
    /// <param name="strFileName">Excel文件全路径(服务器路径)</param>  
    /// <param name="SheetIndex">要获取数据的工作表序号(从0开始)</param>  
    /// <param name="HeaderRowIndex">工作表标题行所在行号(从0开始)</param>  
    /// <returns></returns>  
    public static DataTable RenderDataTableFromExcel(string strFileName, int SheetIndex, int HeaderRowIndex)
    {
        using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
        {
            var ext = Path.GetExtension(strFileName).ToLower();

            IWorkbook workbook;
            if (ext == ".xls")
            {
                workbook = new HSSFWorkbook(file);
            }
            else
            {
                workbook = new XSSFWorkbook(file);
            }

            string SheetName = workbook.GetSheetName(SheetIndex);
            return RenderDataTableFromExcel(workbook, SheetName, HeaderRowIndex);
        }
    }



    /// <summary>  
    /// 从Excel中获取数据到DataTable  
    /// </summary>  
    /// <param name="workbook">要处理的工作薄</param>  
    /// <param name="SheetName">要获取数据的工作表名称</param>  
    /// <param name="HeaderRowIndex">工作表标题行所在行号(从0开始)</param>  
    /// <returns></returns>  
    public static DataTable RenderDataTableFromExcel(IWorkbook workbook, string SheetName, int HeaderRowIndex)
    {
        ISheet sheet = workbook.GetSheet(SheetName);
        DataTable table = new DataTable();
        try
        {
            IRow headerRow = sheet.GetRow(HeaderRowIndex);
            int cellCount = headerRow.LastCellNum;

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                if (headerRow.GetCell(i) == null)
                {
                    break;
                }
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue.Trim());
                table.Columns.Add(column);
            }

            int rowCount = sheet.LastRowNum;

            #region 循环各行各列,写入数据到DataTable
            for (int i = (sheet.FirstRowNum + 1); i < sheet.LastRowNum + 1; i++)
            {
                IRow row = sheet.GetRow(i);
                DataRow dataRow = table.NewRow();
                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    ICell cell = row.GetCell(j);
                    if (cell == null)
                    {
                        dataRow[j] = null;
                    }
                    else
                    {
                        //dataRow[j] = cell.ToString();  
                        switch (cell.CellType)
                        {
                            case CellType.Blank:
                                dataRow[j] = null;
                                break;
                            case CellType.Boolean:
                                dataRow[j] = cell.BooleanCellValue;
                                break;
                            case CellType.Numeric:
                                dataRow[j] = cell.ToString();
                                break;
                            case CellType.String:
                                dataRow[j] = cell.StringCellValue;
                                break;
                            case CellType.Error:
                                dataRow[j] = cell.ErrorCellValue;
                                break;
                            case CellType.Formula:
                            default:
                                dataRow[j] = "=" + cell.CellFormula;
                                break;
                        }
                    }
                }
                table.Rows.Add(dataRow);
                //dataRow[j] = row.GetCell(j).ToString();  
            }
            #endregion
        }
        catch (System.Exception ex)
        {
            table.Clear();
            table.Columns.Clear();
            table.Columns.Add("出错了");
            DataRow dr = table.NewRow();
            dr[0] = ex.Message;
            table.Rows.Add(dr);
            return table;
        }
        finally
        {
            //sheet.Dispose();  
            workbook = null;
            sheet = null;
        }
        #region 清除最后的空行
        for (int i = table.Rows.Count - 1; i > 0; i--)
        {
            bool isnull = true;
            for (int j = 0; j < table.Columns.Count; j++)
            {
                if (table.Rows[i][j] != null)
                {
                    if (table.Rows[i][j].ToString() != "")
                    {
                        isnull = false;
                        break;
                    }
                }
            }
            if (isnull)
            {
                table.Rows[i].Delete();
            }
        }
        #endregion
        return table;
    }



    /// <summary>
    /// 将DataTable数据导出到Excel文件中(xlsx)
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="file"></param>
    public static MemoryStream Export(DataTable dt, string file)
    {
        XSSFWorkbook xssfworkbook = new XSSFWorkbook();
        ISheet sheet = xssfworkbook.CreateSheet();

        //表头
        IRow row = sheet.CreateRow(0);
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            ICell cell = row.CreateCell(i);
            cell.SetCellValue(dt.Columns[i].ColumnName);
        }

        //数据
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            IRow row1 = sheet.CreateRow(i + 1);
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                ICell cell = row1.CreateCell(j);
                cell.SetCellValue(dt.Rows[i][j].ToString());
            }
        }

        using (MemoryStream ms = new MemoryStream())
        {
            xssfworkbook.Write(ms);
            // ms.Flush();
            // ms.Position = 0;
            return ms;
        }

        ////转为字节数组
        //MemoryStream stream = new MemoryStream();
        //xssfworkbook.Write(stream);
        //var buf = stream.ToArray();

        ////保存为Excel文件
        //using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
        //{
        //    fs.Write(buf, 0, buf.Length);
        //    fs.Flush();
        //}

        //return stream;


    }


    /// <summary>
    /// 获取单元格类型(xlsx)
    /// </summary>
    /// <param name="cell"></param>
    /// <returns></returns>
    private static object GetValueTypeForXLSX(XSSFCell cell)
    {
        if (cell == null)
            return null;
        switch (cell.CellType)
        {
            case CellType.Blank: //BLANK:
                return null;
            case CellType.Boolean: //BOOLEAN:
                return cell.BooleanCellValue;
            case CellType.Numeric: //NUMERIC:
                return cell.NumericCellValue;
            case CellType.String: //STRING:
                return cell.StringCellValue;
            case CellType.Error: //ERROR:
                return cell.ErrorCellValue;
            case CellType.Formula: //FORMULA:
            default:
                return "=" + cell.CellFormula;
        }
    }


}