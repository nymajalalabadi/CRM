using CRM.Domain.Entities.Tasks;
using CRM.Domain.ViewModels.Common;

namespace CRM.Domain.ViewModels.Tasks
{
    public class FilterTaskViewModel : Paging<CrmTask>
    {
        public string? FilterOrderName { get; set; }

        public string? FilterCustomerName { get; set; }
    }
}
