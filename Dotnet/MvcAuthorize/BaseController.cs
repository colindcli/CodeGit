using System.Security.Claims;
using System.Web.Mvc;

[AuthorizeFilter]
public abstract class BaseController : Controller
{
    private int Id { get; set; }

    protected int AdminId
    {
        get
        {
            if (Id > 0)
            {
                return Id;
            }
            var claims = HttpContext.User.Identity as ClaimsIdentity;
            if (claims != null)
                Id = int.Parse(claims.AuthenticationType);
            return Id;
        }
    }
}
