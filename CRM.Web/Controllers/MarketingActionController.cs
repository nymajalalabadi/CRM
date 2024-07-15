using CRM.Application.Services.Interface;
using CRM.Domain.ViewModels.MarketingActions;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers
{
    public class MarketingActionController : BaseController
    {
        #region Constructor

        private readonly ITaskService _taskService;

        public MarketingActionController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        #endregion

        #region Actions

        public async Task<IActionResult> FilterAction()
        {
            return View();
        }

        #region Create

        public async Task<IActionResult> CreateAction(long taskId)
        {
            var model = new CreateMarketingActionViewModel()
            {
                CrmTaskId = taskId
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAction(CreateMarketingActionViewModel actionViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData[WarningMessage] = "اطلاعات وارد شده معتبر نمی باشد";
                return View(actionViewModel);
            }

            var result = await _taskService.CreateMarketingAction(actionViewModel);

            switch (result)
            {
                case CreateMarketingActionResult.Success:
                    TempData[SuccessMessage] = "عملیات شما با موفقیت انجام شد";
                    return RedirectToAction("TaskDetail", "Task", new { taskId = actionViewModel.CrmTaskId });

                case CreateMarketingActionResult.Fail:
                    TempData[ErrorMessage] = "عملیات با شکست مواجه شد";
                    break;
            }

            return View(actionViewModel);
        }

        #endregion


        #endregion
    }
}
