//普通数据结构
public class Model
{
    public List<result> results { get; set; }
    public page pagination { get; set; }
    public class result
    {
        public Guid id { get; set; }
        public string text { get; set; }
        public bool selected {get;set;}
        public bool disabled {get;set;}
    }

    public class page
    {
        public bool more { get; set; }
    }
}


//分组数据结构，https://select2.org/data-sources/formats
public class Model
{
    public List<result> results { get; set; }
    public page pagination { get; set; }
    public class result
    {
        public string text { get; set; }
        public List<Item> children {get;set;}
    }

    public class Item
    {
        public Guid id { get; set; }
        public string text { get; set; }
        public bool selected {get;set;}
        public bool disabled {get;set;}
    }

    public class page
    {
        public bool more { get; set; }
    }
}