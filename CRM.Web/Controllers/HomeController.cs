using CRM.Application.Extensions;
using CRM.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CRM.Web.Controllers
{
    public class HomeController : BaseController
    {
        #region MyRegion

        private readonly IProgramSetting _programSetting;

        public HomeController(IProgramSetting programSetting) 
        { 
            _programSetting = programSetting;
        }

        #endregion

        public async Task<IActionResult> Index()
        {
            var result = await _programSetting.FillDashboardViewModel(User.GetUserId());

            return View(result);
        }

    }
}
