using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers
{
	public class UserController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
