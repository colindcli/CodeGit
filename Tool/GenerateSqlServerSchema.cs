using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;

namespace ConsoleApp
{
    /// <summary>
    /// Sql Server数据库架构（表、列、索引、存储过程、函数）生成xml文件
    /// </summary>
    class Program
    {
        private static readonly string ConnectionString = "Data Source=.;uid=test;pwd=123;database=Test;";

        static void Main(string[] args)
        {
            var table = ExcuteTable(SqlStr);
            var xml = table.Rows[0]["Xml"].ToString();
            var reg = new Regex("&#x0D;");
            var text = reg.Replace(xml, "");
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "xml.xml", text);
        }

        public class Row
        {
            public string Name { get; set; }
            public string Key { get; set; }
            public string Value { get; set; }
        }

        /// <summary>
        /// 执行操作返回表
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="type"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        public static DataTable ExcuteTable(string sql, CommandType type = CommandType.Text, params SqlParameter[] ps)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var dt = new DataTable();
                var da = new SqlDataAdapter(sql, conn)
                {
                    SelectCommand = { CommandType = type }
                };
                da.SelectCommand.Parameters.AddRange(ps);
                da.Fill(dt);
                return dt;
            }
        }

        public static string SqlStr
        {
            get
            {
				//sqlserver 2012
                var sqlStr = @"
DECLARE @Tb TABLE(
	Name NVARCHAR(100),
	[Key] NVARCHAR(500),
	Value Nvarchar(MAX)
);

INSERT @Tb
--表、列
SELECT
	'表列' Name,
	x.name [Key],
	y.name Value
FROM sysobjects x
INNER JOIN syscolumns y
	ON x.xtype='U'
	   AND y.id=x.id
ORDER BY x.name ASC,
	y.name ASC;

INSERT @Tb
--表索引
SELECT
	'表索引' Name,
	o.name [Key],
	--i.index_id,
	--i.name,
	CASE WHEN i.type_desc='CLUSTERED' THEN '聚集' ELSE '非聚集' END+': '+SUBSTRING(ikey.cols, 3, LEN(ikey.cols)) Value
FROM sys.objects o
JOIN sys.indexes i
	ON i.object_id=o.object_id
CROSS APPLY
(
	SELECT
		', '+c.name+CASE ic.is_descending_key WHEN 1 THEN ' DESC' ELSE ' ASC' END
	FROM sys.index_columns ic
	JOIN sys.columns c
		ON ic.object_id=c.object_id
		   AND ic.column_id=c.column_id
	WHERE ic.object_id=i.object_id
		  AND ic.index_id=i.index_id
		  AND ic.is_included_column=0
	ORDER BY ic.key_ordinal
	FOR XML PATH('')
) AS ikey(cols)
OUTER APPLY
(
	SELECT
		', '+c.name
	FROM sys.index_columns ic
	JOIN sys.columns c
		ON ic.object_id=c.object_id
		   AND ic.column_id=c.column_id
	WHERE ic.object_id=i.object_id
		  AND ic.index_id=i.index_id
		  AND ic.is_included_column=1
	ORDER BY ic.index_column_id
	FOR XML PATH('')
) AS inc(cols)
WHERE o.object_id IN
	  (
		  SELECT
			  id
		  FROM sys.sysobjects t
		  WHERE t.xtype='U'
	  )
	  AND i.type IN
		  (
			  1, 2
		  )
ORDER BY o.name,
	i.index_id;

INSERT @Tb
--获取所有的存储过程、函数
SELECT
	'存储过程函数' Name,
	b.xtype+'.'+b.name [Key],
	a.text Value
FROM syscomments a
INNER JOIN sysobjects b
	ON b.id=a.id
ORDER BY b.xtype ASC,
	b.name ASC;

SELECT (SELECT * FROM @Tb t FOR XML PATH('Row')) [Xml];
";
                return sqlStr;
            }
        }

    }
}
