using CRM.DataLayer.Context;
using CRM.Domain.Interfaces;
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



        #endregion
    }
}
