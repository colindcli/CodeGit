using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

public class PostEmail
{
    public string Token { get; set; }

    public PostEmail(string token)
    {
        Token = token;
    }

    public string ExceptionSend(string mailTo, string subject, string body)
    {
        var client = new RestClient("http://ms.fzxgj.top/api/Email/ExceptionSend");
        var request = new RestRequest(Method.POST);
        request.AddParameter("TokenId", Token);
        request.AddParameter("EmailTo", mailTo);
        request.AddParameter("Subject", subject);
        request.AddParameter("Body", body);
        var response = client.Execute(request);
        var content = response.Content;
        return content;
    }

    public string StandardSend(string mailTo, string subject, string body)
    {
        var client = new RestClient("http://ms.fzxgj.top/api/Email/StandardSend");
        var request = new RestRequest(Method.POST);
        request.AddParameter("TokenId", Token);
        request.AddParameter("EmailTo", mailTo);
        request.AddParameter("Subject", subject);
        request.AddParameter("Body", body);
        var response = client.Execute(request);
        var content = response.Content;
        return content;
    }
}
