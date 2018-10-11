using System;
using System.IO;
using Microsoft.ClearScript.V8;
using Newtonsoft.Json;

/// <summary>
/// 
/// </summary>
public class JavaScriptHelper
{
    private static readonly string JsCode = File.ReadAllText($"{AppDomain.CurrentDomain.BaseDirectory}sha1pwd.js");

    /// <summary>
    /// 
    /// </summary>
    /// <param name="scriptCode"></param>
    /// <returns></returns>
    public static string Excute(string scriptCode)
    {
        var engine = new V8ScriptEngine();
        return engine.ExecuteCommand(scriptCode);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pwd"></param>
    /// <returns></returns>
    public static SaltTokenModel GetSaltToken(string pwd)
    {
        var code = $"{JsCode};get(\"{pwd}\");";
        SaltTokenModel m;
        while ((m = JsonConvert.DeserializeObject<SaltTokenModel>(Excute(code))).Token.IndexOf("+", StringComparison.OrdinalIgnoreCase) != -1) ;
        return m;
    }
}


//eg.:
//var str = JavaScriptHelper.Excute("function test(){return true;} test();");