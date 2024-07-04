using CRM.Domain.ViewModels.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.Services.Interface
{
    public interface ICompanyService
    {
        #region Methods

        Task<FilterCompanyViewModel> filterCompanyViewModel(FilterCompanyViewModel filter);

        Task<CreateCompanyResult> CreateCompany(CreateCompanyViewModel companyViewModel);

        #endregion
    }
}
