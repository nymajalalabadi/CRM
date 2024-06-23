using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers
{
	public class UserController : Controller
	{
        #region Actions

        #region User List

        public IActionResult Index()
        {
            return View();
        }

        #endregion

        #region Add User

        #region Create Custome

        [HttpGet]
        public async Task<IActionResult> CreateCustomer()
        {
            return View();
        }

        #endregion

        #region Create Marketer

        [HttpGet]
        public async Task<IActionResult> CreateMarketer()
        {
            return View();
        }

        #endregion


        #endregion

        #endregion
    }
}
