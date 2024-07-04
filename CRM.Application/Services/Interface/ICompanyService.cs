using CRM.Domain.ViewModels.Company;

namespace CRM.Application.Services.Interface
{
    public interface ICompanyService
    {
        #region Methods

        Task<FilterCompanyViewModel> filterCompanyViewModel(FilterCompanyViewModel filter);

        Task<CreateCompanyResult> CreateCompany(CreateCompanyViewModel companyViewModel);

        Task<EditCompanyViewModel> GetCompanyByEdit(long companyId);

        Task<EditCompanyResult> EditCompany(EditCompanyViewModel edit);

        Task<bool> DeleteCompany(long companyId);

        #endregion
    }
}
