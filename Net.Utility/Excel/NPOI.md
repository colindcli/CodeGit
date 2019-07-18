## 修改Excel并导出

- 使用组件：[NPOI](https://github.com/dotnetcore/NPOI) / [exmaple](https://github.com/tonyqus/npoi/tree/master/examples)

> 读取

    IWorkbook workbook = new HSSFWorkbook(file);
    IWorkbook workbook = new XSSFWorkbook(file);

> 修改Excel

    var sheetUser = workbook.GetSheetAt(0);
    for (int i = 0; i < user.Rows.Count; i++)
    {
        IRow row1 = sheetUser.CreateRow(i + 2);
        for (int j = 0; j < user.Columns.Count; j++)
        {
            ICell cell = row1.CreateCell(j);
            cell.SetCellValue(user.Rows[i][j].ToString());
        }
    }


> 写文件

    var fs = new FileStream(path, FileMode.Create, FileAccess.Write);
    workbook.Write(fs);
    fs.Close();
    fs.Dispose();


> 导出Excel

    return new FilePathResult(path, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
    {
        FileDownloadName = "Export.xls"
    };