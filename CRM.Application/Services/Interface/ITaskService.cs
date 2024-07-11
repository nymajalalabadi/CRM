using CRM.Domain.ViewModels.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.Services.Interface
{
    public interface ITaskService
    {
        #region Methods

        Task<FilterTaskViewModel> FilterTasks(FilterTaskViewModel filter);

        Task<CreateTaskResult> CreateTask(CreateTaskViewModel createTask);

        Task<EditTaskViewModel> GetTaskForEdit(long taskId);

        Task<EditTaskResult> EditTask(EditTaskViewModel editTask);

        Task<bool> DeleteTask(long taskId);

        #endregion
    }
}
