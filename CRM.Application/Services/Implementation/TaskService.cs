using CRM.Application.Convertors;
using CRM.Application.Services.Interface;
using CRM.Domain.Entities.Tasks;
using CRM.Domain.Interfaces;
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

            query = query.OrderByDescending(t => t.CreateDate);

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

            var result =  new EditTaskViewModel()
            {
                TaskId = task.TaskId,
                MarketerId= task.MarketerId,
                OrderId= task.OrderId,
                Description = task.Description,
                Priority = task.Priority,
                UntilDate = task.UntilDate.ToShamsiDate(),
                CrmTaskStatus = task.CrmTaskStatus
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

        #endregion
    }
}
