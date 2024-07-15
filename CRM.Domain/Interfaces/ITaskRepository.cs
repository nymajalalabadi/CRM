using CRM.Domain.Entities.Actions;
using CRM.Domain.Entities.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Interfaces
{
    public interface ITaskRepository
    {
        #region Methods

        #region Task

        Task<IQueryable<CrmTask>> GetTasks();

        Task AddTask(CrmTask task);

        void UpdateTask(CrmTask task);

        Task<CrmTask?> GetTaskById(long taskId);

        Task<CrmTask?> GetTask(long taskId);

        Task SaveChanges();

        #endregion

        #region Marketing Action

        Task AddAction(MarketingAction action);

        void UpdateAction(MarketingAction action);

        Task<MarketingAction?> GetActionById(long actionId);

        Task<IQueryable<MarketingAction>> GetMarketingActions(long taskId);

        #endregion


        #endregion
    }
}
