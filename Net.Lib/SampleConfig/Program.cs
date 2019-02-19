using System.Collections.Generic;
using System.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {
        var settings = (ServiceSettings)(dynamic)ConfigurationManager.GetSection("serviceSettings");
    }
}

public class ServiceSettings
{
    public int MaxThreads { get; set; }
    public string Endpoint { get; set; }
    public IEnumerable<string> BannedPhrases { get; set; }
}
