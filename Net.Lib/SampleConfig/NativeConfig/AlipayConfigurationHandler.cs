using System.Collections.Generic;
using System.Configuration;
using System.Xml;

namespace ConsoleApp
{
    public class AlipayConfigurationHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            var dict = new Dictionary<string, string>();
            if (section.Attributes != null)
            {
                dict.Add("gatewayUrl", section.Attributes["gatewayUrl"].Value);
            }

            var childs = section.ChildNodes;
            foreach (XmlNode item in childs)
            {
                dict.Add(item.Name, item.InnerText);
            }
            return dict;
        }
    }
}
