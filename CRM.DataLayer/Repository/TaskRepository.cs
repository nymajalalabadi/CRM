using CRM.DataLayer.Context;
using CRM.Domain.Entities.Actions;
using CRM.Domain.Entities.Tasks;
using CRM.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DataLayer.Repository
{
    public class TaskRepository : ITaskRepository
    {
        #region Constructor

        private readonly CrmContext _context;

        public TaskRepository(CrmContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        #region Task

        public async Task<IQueryable<CrmTask>> GetTasks()
        {
            return _context.CrmTasks.Where(t => !t.IsDelete)
                .Include(t => t.Order)
                .ThenInclude(t => t.Customer)
                .ThenInclude(t => t.User)
                .Include(t => t.Marketer)
                .AsQueryable();
        }

        public async Task AddTask(CrmTask task)
        {
            await _context.CrmTasks.AddAsync(task);
        }

        public void UpdateTask(CrmTask task)
        {
            _context.CrmTasks.Update(task);
        }

        public async Task<CrmTask?> GetTaskById(long taskId)
        {
            return await _context.CrmTasks.FirstOrDefaultAsync(t => t.TaskId.Equals(taskId));
        }

        public async Task<CrmTask?> GetTask(long taskId)
        {
            return await _context.CrmTasks
                .Include(t => t.Marketer).ThenInclude(m => m.User)
                .Include(t => t.MarketingActions)
                .FirstOrDefaultAsync(t => t.TaskId.Equals(taskId));
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        #endregion

        #region Marketing Action

        public async Task AddAction(MarketingAction action)
        {
            await _context.MarketingActions.AddAsync(action);
        }

        public void UpdateAction(MarketingAction action)
        {
            _context.MarketingActions.Update(action);
        }

        public async Task<MarketingAction?> GetActionById(long actionId)
        {
            return await _context.MarketingActions
                .Include(a => a.CrmTask)
                .FirstOrDefaultAsync(a => a.ActionId.Equals(actionId));
        }

        public async Task<IQueryable<MarketingAction>> GetMarketingActions(long taskId)
        {
            return  _context.MarketingActions.Where(a => !a.IsDelete && a.CrmTaskId.Equals(taskId)).AsQueryable();
        }

        #endregion

        #endregion
    }
}
