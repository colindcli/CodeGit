public class HomeController : Controller
{
    private readonly IHelloModel _helloModel;
    public HomeController(IHelloModel helloModel)
    {
        _helloModel = helloModel;
    }

    public ActionResult Index()
    {
        var helloworld = _helloModel.GetName();
        return View();
    }
}
