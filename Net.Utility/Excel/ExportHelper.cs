using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

/// <summary>
/// 导出Excel
/// Install-Package NPOI
/// </summary>
public class ExportHelper
{
    /// <summary>
    /// 导出Excel
    /// </summary>
    /// <param name="lists"></param>
    /// <param name="fileName">Excel文件全路径(服务器路径)</param>
    /// <param name="sheetIndex">要获取数据的工作表序号(从0开始)</param>
    /// <param name="headerRowIndex">工作表标题行所在行号(从0开始)</param>
    /// <returns></returns>
    public static byte[] ExportExcel<T>(List<T> lists, string fileName, int sheetIndex = 0, int headerRowIndex = 1) where T : class
    {
        if (!File.Exists(fileName))
        {
            throw new Exception("文件不存在！");
        }

        var ext = Path.GetExtension(fileName)?.ToLower();
        IWorkbook workbook;
        using (var file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
        {
            if (ext == ".xls")
            {
                workbook = new HSSFWorkbook(file);
            }
            else
            {
                workbook = new XSSFWorkbook(file);
            }
        }
        var sheet = workbook.GetSheetAt(sheetIndex);

        var propertyList = typeof(T).GetProperties();
        for (var i = 0; i < lists.Count; i++)
        {
            var row = sheet.CreateRow(i + 1);
            for (var j = 0; j < propertyList.Length; j++)
            {
                var cell = row.CreateCell(j);
                var obj = propertyList[j].GetValue(lists[i]);
                if (obj is DateTime dt)
                {
                    cell.SetCellValue(dt);
                    continue;
                }

                if (obj is double d)
                {
                    cell.SetCellValue(d);
                    continue;
                }

                if (obj is decimal de)
                {
                    if (double.TryParse(de.ToString(CultureInfo.InvariantCulture), out var v))
                    {
                        cell.SetCellValue(v);
                    }
                    else
                    {
                        cell.SetCellValue(obj.ToString());
                    }
                    continue;
                }

                if (obj is int it)
                {
                    if (double.TryParse(it.ToString(CultureInfo.InvariantCulture), out var v))
                    {
                        cell.SetCellValue(v);
                    }
                    else
                    {
                        cell.SetCellValue(obj.ToString());
                    }
                    continue;
                }

                if (obj is long l)
                {
                    if (double.TryParse(l.ToString(CultureInfo.InvariantCulture), out var v))
                    {
                        cell.SetCellValue(v);
                    }
                    else
                    {
                        cell.SetCellValue(obj.ToString());
                    }
                    continue;
                }

                if (obj is bool b)
                {
                    cell.SetCellValue(b);
                    continue;
                }

                cell.SetCellValue(obj.ToString());
            }
        }

        using (var ms = new MemoryStream())
        {
            workbook.Write(ms);
            return ms.ToArray();
        }
    }
}