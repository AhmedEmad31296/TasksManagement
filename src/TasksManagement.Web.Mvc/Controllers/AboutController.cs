using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using TasksManagement.Controllers;

namespace TasksManagement.Web.Controllers
{
    [AbpMvcAuthorize]
    public class AboutController : TasksManagementControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
