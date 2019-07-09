//https://www.nuget.org/packages/Npoi.Mapper/
//http://donnytian.github.io/Npoi.Mapper/
//Install-Package Npoi.Mapper

using Npoi.Mapper;
using Npoi.Mapper.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 读取Excel
/// </summary>
public class ImportHelper
{
    /// <summary>
    /// 读取Excel
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fileName"></param>
    /// <param name="errors"></param>
    /// <returns></returns>
    public static List<T> ReadSheet<T>(string fileName, ref List<string> errors) where T : class
    {
        if (errors == null)
        {
            errors = new List<string>();
        }
        var mapper = new Mapper(fileName)
        {
            HasHeader = true
        };
        var sheet = mapper.Take<T>().ToList();
        errors = sheet.Where(p => p.ErrorColumnIndex > -1).Select(m => $"第{++m.RowNumber}行第{++m.ErrorColumnIndex}列错误").ToList();
        return sheet.Select(p => p.Value).ToList();
    }
}

/// <summary>
/// Demo
/// </summary>
public class SampleClass
{
    [Column(0)]
    public int Id { get; set; }
    [Column(1)]
    public string Name { get; set; }
    [Column(2)]
    public DateTime Date { get; set; }
}