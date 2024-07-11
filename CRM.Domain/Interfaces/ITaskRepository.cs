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

        Task<IQueryable<CrmTask>> GetTasks();

        Task AddTask(CrmTask task);

        void UpdateTask(CrmTask task);

        Task<CrmTask?> GetTaskById(long taskId);

        Task SaveChanges();

        #endregion
    }
}
