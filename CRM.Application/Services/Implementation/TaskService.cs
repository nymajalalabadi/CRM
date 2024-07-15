using CRM.Application.Convertors;
using CRM.Application.Services.Interface;
using CRM.Domain.Entities.Actions;
using CRM.Domain.Entities.Tasks;
using CRM.Domain.Interfaces;
using CRM.Domain.ViewModels.MarketingActions;
using CRM.Domain.ViewModels.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.Services.Implementation
{
    public class TaskService : ITaskService
    {
        #region Constructor

        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        #endregion

        #region Methods

        #region Task

        public async Task<FilterTaskViewModel> FilterTasks(FilterTaskViewModel filter)
        {
            var query = await _taskRepository.GetTasks();

            #region Filter

            if (!string.IsNullOrEmpty(filter.FilterCustomerName))
            {
                query = query.Where(a => a.Order != null && EF.Functions.Like(a.Order.Customer.User.FirstName + " " + a.Order.Customer.User.LastName + " " + a.Order.Customer.User.UserName, $"%{filter.FilterCustomerName}%"));
            }

            if (!string.IsNullOrEmpty(filter.FilterOrderName))
            {
                query = query.Where(a => a.Order != null && EF.Functions.Like(a.Order.Title, $"%{filter.FilterOrderName}%"));
            }

            #endregion

            query = query.OrderBy(t => t.Priority);

            #region paging

            await filter.SetPaging(query);

            #endregion

            return filter;
        }

        public async Task<CreateTaskResult> CreateTask(CreateTaskViewModel createTask)
        {

            if (createTask.Priority == 0)
            {
                return CreateTaskResult.Fail;
            }

            var task = new CrmTask()
            {
                Description = createTask.Description,
                OrderId = createTask.OrderId,
                Priority = createTask.Priority,
                UntilDate = createTask.UntilDate.ToMiladiDate(),
                MarketerId = createTask.MarketerId,
                CrmTaskStatus = CrmTaskStatus.Waiting,
            };

            await _taskRepository.AddTask(task);
            await _taskRepository.SaveChanges();

            return CreateTaskResult.Success;
        }

        public async Task<EditTaskViewModel> GetTaskForEdit(long taskId)
        {
            var task = await _taskRepository.GetTaskById(taskId);

            if (task == null)
            {
                return null;
            }

            var result = new EditTaskViewModel()
            {
                TaskId = task.TaskId,
                Description = task.Description,
                Priority = task.Priority,
                CrmTaskStatus = task.CrmTaskStatus,
                UntilDate = task.UntilDate.ToShamsiDate()
            };

            return result;
        }

        public async Task<EditTaskResult> EditTask(EditTaskViewModel editTask)
        {
            var task = await _taskRepository.GetTaskById(editTask.TaskId);

            if (task == null)
            {
                return EditTaskResult.Fail;
            }

            task.Description = editTask.Description;
            task.Priority = editTask.Priority;
            task.CrmTaskStatus = editTask.CrmTaskStatus;
            task.UntilDate = editTask.UntilDate.ToMiladiDate();

            _taskRepository.UpdateTask(task);
            await _taskRepository.SaveChanges();

            return EditTaskResult.Success;
        }

        public async Task<bool> DeleteTask(long taskId)
        {
            var task = await _taskRepository.GetTaskById(taskId);

            if (task == null)
            {
                return false;
            }

            task.IsDelete = true;

            _taskRepository.UpdateTask(task);
            await _taskRepository.SaveChanges();

            return true;
        }

        public async Task<TaskDetailViewModel> FillTaskDetailViewModel(long taskId)
        {
            var task = await _taskRepository.GetTask(taskId);

            if (task == null)
            {
                return null;
            }

            return new TaskDetailViewModel()
            {
                TaskId = task.TaskId,
                Description = task.Description,
                CrmTaskStatus = task.CrmTaskStatus,
                UntilDate = task.UntilDate.ToShamsiDate(),
                Priority = task.Priority,
                CreateDate = task.CreateDate.ToShamsiDate(),
                User = task.Marketer.User,
            };
        }

        public async Task<bool> ChangeTaskState(long taskId, CrmTaskStatus crmTaskStatus)
        {
            var task = await _taskRepository.GetTaskById(taskId);

            if (task == null)
            {
                return false;
            }

            task.CrmTaskStatus = crmTaskStatus;

            _taskRepository.UpdateTask(task);
            await _taskRepository.SaveChanges();

            return true;
        }

        #endregion

        #region Marketing Action

        public async Task<FilterMarketingActionViewModel> FilterActions(FilterMarketingActionViewModel filter)
        {
            var query = await _taskRepository.GetMarketingActions(filter.TaskId);

            query = query.OrderByDescending(a => a.CreateDate);

            #region Paging

            await filter.SetPaging(query);

            #endregion

            return filter;
        }

        public async Task<CreateMarketingActionResult> CreateMarketingAction(CreateMarketingActionViewModel action)
        {
            var task = await _taskRepository.GetTaskById(action.CrmTaskId);

            if (task == null)
            {
                return CreateMarketingActionResult.Fail;
            }

            var newAction = new MarketingAction()
            {
                CrmTaskId = action.CrmTaskId,
                Description = action.Description,
            };

            await _taskRepository.AddAction(newAction);
            await _taskRepository.SaveChanges();

            return CreateMarketingActionResult.Success;
        }

        public async Task<EditMarketingActionViewModel> GetMarketingActionForEdit(long actionId)
        {
            var action = await _taskRepository.GetActionById(actionId);

            if (action == null)
            {
                return null;
            }

            return new EditMarketingActionViewModel()
            {
                ActionId = action.ActionId,
                CrmTaskId = action.CrmTaskId,
                Description = action.Description!
            };
        }

        public async Task<EditMarketingActionResult> EditMarketingAction(EditMarketingActionViewModel action)
        {
            var currentAction = await _taskRepository.GetActionById(action.ActionId);

            if (currentAction == null)
            {
                return EditMarketingActionResult.Fail;
            }

            currentAction.Description = action.Description;

            _taskRepository.UpdateAction(currentAction);
            await _taskRepository.SaveChanges();

            return EditMarketingActionResult.Success;
        }

        public async Task<bool> DeleteAction(long actionId)
        {
            var action = await _taskRepository.GetActionById(actionId);

            if (action == null)
            {
                return false;
            }

            action.IsDelete = true;

            _taskRepository.UpdateAction(action);
            await _taskRepository.SaveChanges();

            return true;
        }

        #endregion

        #endregion
    }
}
