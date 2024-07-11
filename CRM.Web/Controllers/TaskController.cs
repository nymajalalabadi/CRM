using CRM.Application.Services.Interface;
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

        #region filter

        public IActionResult Index()
        {
            return View();
        }

        #endregion
    }
}
