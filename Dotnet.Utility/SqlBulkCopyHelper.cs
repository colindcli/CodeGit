using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

public class SqlBulkCopyHelper
{
    /// <summary>
    /// List转DataTable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="tableName"></param>
    /// <returns></returns>
    private static DataTable ToDataTable<T>(List<T> list, string tableName)
    {
        var tb = new DataTable
        {
            TableName = tableName
        };
        var props = typeof(T).GetProperties().ToList();
        foreach (var prop in props)
        {
            //var type = prop.PropertyType;
            //if (type.IsGenericType)
            //{
            //    type = type.GetGenericArguments()[0];
            //}
            var col = new DataColumn(prop.Name, typeof(string))
            {
                AllowDBNull = true
            };
            tb.Columns.Add(col);
        }
        foreach (var item in list)
        {
            var row = tb.NewRow();
            foreach (var prop in props)
            {
                var val = prop.GetValue(item);
                row[prop.Name] = val ?? DBNull.Value;
            }
            tb.Rows.Add(row);
        }
        return tb;
    }

    /// <summary>
    /// 批量写入数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="bulk"></param>
    /// <param name="list"></param>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public static async Task WriteToServerAsync<T>(SqlBulkCopy bulk, List<T> list, string tableName)
    {
        var tb = ToDataTable(list, tableName);
        bulk.DestinationTableName = tb.TableName;
        foreach (DataColumn col in tb.Columns)
        {
            bulk.ColumnMappings.Add(col.ColumnName, col.ColumnName);
        }
        await bulk.WriteToServerAsync(tb);
    }

    /// <summary>
    /// 批量写入数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="bulk"></param>
    /// <param name="list"></param>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public static bool WriteToServer<T>(SqlBulkCopy bulk, List<T> list, string tableName)
    {
        try
        {
            var tb = ToDataTable(list, tableName);
            bulk.DestinationTableName = tb.TableName;
            foreach (DataColumn col in tb.Columns)
            {
                bulk.ColumnMappings.Add(col.ColumnName, col.ColumnName);
            }
            bulk.WriteToServer(tb);
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
