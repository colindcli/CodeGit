using System.Security.Claims;
using System.Web.Mvc;

[AuthorizeFilter]
public abstract class BaseController : Controller
{
    public int AdminId { get; set; }
}
