using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

public class Translate
{
    public static async Task<TranslateResponse> TranslateText(string input, lang langInput = lang.auto, lang langOutput = lang.zh_CN)
    {
        var res = new TranslateResponse();
        try
        {
            var client = new HttpClient();
            const string sl = "auto";
            var tl = langOutput.ToString().Replace("_", "-");
            var hl = langInput.ToString().Replace("_", "-");
            var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={sl}&tl={tl}&hl={hl}&dt=t&dt=bd&dj=1&source=input&tk=501776.501776&q={input}";
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var text = await response.Content.ReadAsStringAsync();
                var translateResult = JsonConvert.DeserializeObject<TranslateResult>(text);
                res.IsSuccess = true;
                res.Result = translateResult;
                res.Response = text;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("GoogleTranslateLib.Translate [error] = " + ex);
            res.IsSuccess = false;
            res.Exception = ex;
            res.MessageError = ex.Message;
        }
        return res;
    }

    public static Dictionary<lang, string> GetLangusges()
    {
        var res = new Dictionary<lang, string>();
        var type = typeof(lang);
        foreach (lang item in Enum.GetValues(type))
        {
            var description = type.GetField(item.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), true).Cast<DescriptionAttribute>().First().Description;
            res.Add(item, description);
        }
        return res;
    }
}