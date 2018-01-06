public class Model
{
    public Guid id { get; set; }
    public Guid pId { get; set; }
    public string name { get; set; }
    public bool open
    {
        get { return pId == Guid.Empty; }
    }
}
