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

        #region Edit

        [HttpGet]
        public async Task<IActionResult> EditTask(long taskId)
        {
            var model = await _taskService.GetTaskForEdit(taskId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditTask(EditTaskViewModel editTask)
        {
            if (!TryValidateModel(editTask))
            {
                TempData[WarningMessage] = "اطلاعات وارد شده معتبر نمی باشد";
                return View(editTask);
            }

            var result = await _taskService.EditTask(editTask);

            switch (result)
            {
                case EditTaskResult.Success:
                    TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
                    return RedirectToAction("FilterTask");

                case EditTaskResult.Fail:
                    TempData[ErrorMessage] = "عملیات با شکست مواجه شد";
                    break;
            }

            return View(editTask);
        }

        #endregion

        #region Delete

        public async Task<IActionResult> DeleteTask(long taskId)
        {
            var result = await _taskService.DeleteTask(taskId);

            if (result)
            {
                TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
            }
            else
            {
                TempData[ErrorMessage] = "عملیات با شکست مواجه شد";
            }

            return RedirectToAction("FilterTask");
        }

        #endregion

        #endregion
    }
}
