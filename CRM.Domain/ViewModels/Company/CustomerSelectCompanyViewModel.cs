using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.ViewModels.Company
{
    public class CustomerSelectCompanyViewModel
    {
        public long CustomerId { get; set; }

        public long CompanyId { get; set; }
    }

    public enum AddCustomerSelectCompanyResult
    {
        Success,
        Fail,
        SelectedCustomerExist
    }
}
