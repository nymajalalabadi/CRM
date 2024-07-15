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

        #region Edit Marketing Action

        public async Task<IActionResult> EditAction(long actionId)
        {
            var model = await _taskService.GetMarketingActionForEdit(actionId);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditAction(EditMarketingActionViewModel editMarketingAction)
        {
            if (!ModelState.IsValid)
            {
                TempData[WarningMessage] = "اطلاعات وارد شده معتبر نمی باشد";
                return View(editMarketingAction);
            }

            var result = await _taskService.EditMarketingAction(editMarketingAction);

            switch (result)
            {
                case EditMarketingActionResult.Success:
                    TempData[SuccessMessage] = "عملیات شما با موفقیت انجام شد";
                    return RedirectToAction("TaskDetail", "Task", new { taskId = editMarketingAction.CrmTaskId });

                case EditMarketingActionResult.Fail:
                    TempData[ErrorMessage] = "عملیات با شکست مواجه شد";
                    break;
            }

            return View(editMarketingAction);
        }

        #endregion

        #region Delete Action

        public async Task<IActionResult> DeleteAction(long actionId)
        {
            var result = await _taskService.DeleteAction(actionId);

            if (result)
            {
                TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
            }
            else
            {
                TempData[ErrorMessage] = "عملیات با شکست مواجه شد";
            }

            return RedirectToAction("FilterTask", "Task");
        }

        #endregion

        #endregion
    }
}
