﻿using CRM.Domain.Entities.Tasks;
using CRM.Domain.ViewModels.MarketingActions;
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

        #region Task

        Task<FilterTaskViewModel> FilterTasks(FilterTaskViewModel filter);

        Task<CreateTaskResult> CreateTask(CreateTaskViewModel createTask);

        Task<EditTaskViewModel> GetTaskForEdit(long taskId);

        Task<CrmTask?> GetTaskbyId(long taskId);

        Task<EditTaskResult> EditTask(EditTaskViewModel editTask);

        Task<bool> DeleteTask(long taskId);

        Task<TaskDetailViewModel> FillTaskDetailViewModel(long taskId);

        Task<bool> ChangeTaskState(long taskId, CrmTaskStatus crmTaskStatus);

        #endregion

        #region Marketing Action

        Task<CreateMarketingActionResult> CreateMarketingAction(CreateMarketingActionViewModel action);

        Task<EditMarketingActionViewModel> GetMarketingActionForEdit(long actionId);

        Task<EditMarketingActionResult> EditMarketingAction(EditMarketingActionViewModel action);

        Task<bool> DeleteAction(long actionId);

        #endregion

        #endregion
    }
}
