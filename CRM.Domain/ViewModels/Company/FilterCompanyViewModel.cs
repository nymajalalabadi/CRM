using CRM.Domain.Entities.Companies;
using CRM.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.ViewModels.Company
{
    public class FilterCompanyViewModel : Paging<Entities.Companies.Company>
    {
        public string? FilterCompanyName { get; set; }

        public string? FilterCompanyCode { get; set; }
    }
}
