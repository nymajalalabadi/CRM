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

        #endregion
    }
}
