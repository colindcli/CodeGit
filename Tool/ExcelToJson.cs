//Install-Package ExcelDataReader -Version 3.4.0
//Install-Package ExcelDataReader.DataSet -Version 3.4.0
//Install-Package Newtonsoft.Json -Version 11.0.2
using ExcelDataReader;
using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;

//Excel转json数据
namespace GenerateJsonData
{
    class Program
    {
        static void Main(string[] args)
        {
            //excel第一行Json名，第二行开始Json值
            var filePath = AppDomain.CurrentDomain.BaseDirectory + "1.xlsx";
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();
                    var tb = result.Tables[0];
                    if (tb.Rows.Count < 1)
                        return;

                    var table = new DataTable();
                    for (var m = 0; m < tb.Columns.Count; m++)
                    {
                        var type = tb.Rows[1][m].GetType();
                        if (type == typeof(double))
                        {
                            var isInt = true;
                            for (var j = 1; j < tb.Rows.Count; j++)
                            {
                                var v = (double)tb.Rows[j][m];
                                var it = 0;
                                if (int.TryParse(v.ToString(), out it))
                                {
                                    if (it == v)
                                    {
                                        continue;
                                    }
                                }

                                isInt = false;
                                break;
                            }

                            table.Columns.Add(tb.Rows[0][m].ToString(), isInt ? typeof(int) : typeof(double));
                        }
                        else
                        {
                            table.Columns.Add(tb.Rows[0][m].ToString(), type);
                        }
                    }
                    for (var j = 1; j < tb.Rows.Count; j++)
                    {
                        var row = table.NewRow();
                        for (var m = 0; m < tb.Columns.Count; m++)
                        {
                            row[m] = tb.Rows[j][m];
                        }

                        table.Rows.Add(row);
                    }
                    var json = JsonConvert.SerializeObject(table);
                    File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "data.json", json);
                }
            }
        }
    }
}
