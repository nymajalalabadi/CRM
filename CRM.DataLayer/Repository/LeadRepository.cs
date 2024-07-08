using CRM.DataLayer.Context;
using CRM.Domain.Entities.Leads;
using CRM.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DataLayer.Repository
{
    public class LeadRepository : ILeadRepository
    {
        #region Constructor

        private CrmContext _context;

        public LeadRepository(CrmContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        public async Task<IQueryable<Lead>> GetLeads()
        {
            return _context.Leads.Where(l => !l.IsDelete)
                .Include(c => c.Owner)
                .AsQueryable();
        }

        public async Task<Lead?> GetLeadById(long leadId)
        {
            return await _context.Leads.FirstOrDefaultAsync(l => l.LeadId.Equals(leadId));
        }

        public async Task AddLead(Lead lead)
        {
            await _context.Leads.AddAsync(lead);
        }

        public void UpdateLead(Lead lead)
        {
            _context.Leads.Update(lead);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
