
/*
<?xml version="1.0" encoding="utf-8" ?>
<datas>
  <!--类型-->
  <data id="1">
    <d>问题1</d>
  </data>
</datas>
*/


/// <summary>
/// 
/// </summary>
/// <param name="id"></param>
/// <returns></returns>
public static IEnumerable<string> GetDropDownDataByMoudleId(string id)
{
    var xml = XElement.Load(System.Web.HttpContext.Current.Server.MapPath(string.Format("~/App_Data/ConfigData/DropdownList.xml")));

    var linq = from q in
                    (from x in xml.Descendants("data")
                    let xAttribute = x.Attribute("id")
                    where xAttribute != null && xAttribute.Value == id
                    select x).Elements("d")
                select q.Value;

    return linq;
}
