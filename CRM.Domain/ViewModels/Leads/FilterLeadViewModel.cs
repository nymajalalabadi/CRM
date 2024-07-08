using CRM.Domain.Entities.Leads;
using CRM.Domain.ViewModels.Common;
using CRM.Domain.ViewModels.Enums;

namespace CRM.Domain.ViewModels.Leads
{
    public class FilterLeadViewModel : Paging<Lead>
    {
        public string? FilterLeadTopic { get; set; }
        public string? FilterLeadName { get; set; }
        public FilterLeadState FilterLeadState { get; set; }
    }
}
