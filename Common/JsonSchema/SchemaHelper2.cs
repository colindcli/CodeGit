using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Text;

public class SchemaHelper
{
    public static string GetClassSchema(Type t)
    {
        var generator = new JSchemaGenerator();
        var schema = generator.Generate(t);
        var json = GetJSchema(schema);
        return json;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="schema"></param>
    /// <param name="depth"></param>
    /// <returns></returns>
    private static string GetJSchema(JSchema schema, int depth = 1)
    {
        if (schema.Properties == null)
            return schema.Type.ToString();
        
        var sb = new StringBuilder();
        sb.AppendLine("{");
        foreach (var item in schema.Properties)
        {
            var key = item.Key;
            var value = item.Value;
            var v = value.Type.ToString();
            var array = v.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (string.Equals(array[0], "Array", StringComparison.OrdinalIgnoreCase))
            {
                var lists = new List<string>();
                foreach (var valueItem in value.Items)
                {
                    var json = GetJSchema(valueItem, depth + 1);
                    lists.Add(json);
                }
                sb.AppendLine($"{GetTab(depth)}{key}:[{string.Join(",", lists)}]");
            }
            else
            {
                sb.AppendLine($"{GetTab(depth)}{key}:{array[0]}");
            }

        }
        if (depth == 1)
        {
            sb.AppendLine("}");
        }
        else
        {

            sb.Append($"{GetTab(depth - 1)}}}");
        }
        return sb.ToString();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="depth"></param>
    /// <returns></returns>
    private static string GetTab(int depth)
    {
        var tab = "";
        for (var i = 0; i < depth; i++)
        {
            tab += "    ";
        }
        return tab;
    }
}
