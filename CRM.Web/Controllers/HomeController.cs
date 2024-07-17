using CRM.Application.Extensions;
using CRM.Application.Services.Implementation;
using CRM.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CRM.Web.Controllers
{
    public class HomeController : BaseController
    {
        #region Constructor

        private readonly IProgramSetting _programSetting;
        private readonly IPredictService _predictService;

        public HomeController(IProgramSetting programSetting, IPredictService predictService) 
        { 
            _programSetting = programSetting;
            _predictService = predictService;
        }

        #endregion

        public async Task<IActionResult> Index()
        {
            var result = await _programSetting.FillDashboardViewModel(User.GetUserId());

            return View(result);
        }

        public async Task<IActionResult> ProcessPredictMarketer()
        {
            var result = await _predictService.ProcessMarketerPredict();

            if (result)
            {
                TempData[SuccessMessage] = "عمیات با موفقیت انجام شد";
            }
            else
            {
                TempData[ErrorMessage] = "عمیات با شکست مواجه شد";
            }

            return RedirectToAction("Index");
        }

    }
}
