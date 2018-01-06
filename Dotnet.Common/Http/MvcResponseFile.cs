public class FileController : BaseController
{
    // GET: File
    public ActionResult Index(Guid id)
    {
        var path = $"";
        return new FilePathResult(path, "application/octet-stream")
        {
            FileDownloadName = "FileName"
        };
    }
}
