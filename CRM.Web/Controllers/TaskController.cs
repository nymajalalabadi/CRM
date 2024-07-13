using CRM.Application.Extensions;
using CRM.Application.Services.Interface;
using CRM.Domain.ViewModels.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers
{
    public class TaskController : BaseController
    {
        #region Constructor

        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        #endregion

        #region Actions

        #region Filter

        public async Task<IActionResult> FilterTask(FilterTaskViewModel filter)
        {
            var model = await _taskService.FilterTasks(filter);

            return View(model);
        }

        #endregion

        #region Create

        public async Task<IActionResult> CreateTask(long? orderId)
        {
            var model = new CreateTaskViewModel();

            if (orderId != null)
            {
                model.OrderId = orderId.Value;
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskViewModel createTask)
        {
            if (!TryValidateModel(createTask))
            {
                TempData[WarningMessage] = "اطلاعات وارد شده معتبر نمیباشد";
                return View(createTask);
            }

            createTask.MarketerId = User.GetUserId();

            var result = await _taskService.CreateTask(createTask);

            switch (result)
            {
                case CreateTaskResult.Success:
                    TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
                    return RedirectToAction("FilterTask");
                case CreateTaskResult.Fail:
                    TempData[ErrorMessage] = "عملیات با شکست مواجه شد";
                    break;
            }

            return View(createTask);
        }
        #endregion

        #endregion
    }
}
