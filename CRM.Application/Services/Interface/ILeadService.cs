using CRM.Domain.ViewModels.Leads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.Services.Interface
{
    public interface ILeadService
    {
        #region Methods

        Task<FilterLeadViewModel> FilterLeads(FilterLeadViewModel filter);

        Task<CreateLeadResult> CreateLead(CreateLeadViewModel createLead, long userId);

        Task<EditLeadViewModel> FillEditLeadViewModel(long leadId);

        Task<EditLeadResult> EditLead(EditLeadViewModel editLead);

        Task<bool> DeleteLead(long leadId);

        Task<AddleadSelectMarketerResult> SetLeadToMarketer(LeadSelectMarketerViewModel leadSelectMarketer);

        Task<bool> SetLeadToMarketer(long leadId, long marketerId);

        #endregion
    }
}
