public class Model
{
    public List<result> results { get; set; }
    public page pagination { get; set; }
    public class result
    {
        public Guid id { get; set; }
        public string text { get; set; }
    }

    public class page
    {
        public bool more { get; set; }
    }
}
