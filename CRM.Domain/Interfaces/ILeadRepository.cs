using CRM.Domain.Entities.Leads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Interfaces
{
    public interface ILeadRepository
    {
        #region Methods

        Task<IQueryable<Lead>> GetLeads();

        Task<Lead?> GetLeadById(long leadId);

        Task AddLead(Lead lead);

        void UpdateLead(Lead lead);

        Task SaveChanges();

        #endregion
    }
}
