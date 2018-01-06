using System;
using System.Web.Http;
using WeProject.Entity;

namespace WeProject.WebApiPc.Controllers
{
    public abstract class BaseController : ApiController
    {
        protected static readonly string Root = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('/', '\\');

        public StaffModel StaffModel { get; set; }

        protected BaseController()
        {
            StaffModel = new StaffModel()
            {
                UserId = 1,
                CompanyId = 1
            };
        }
    }
}