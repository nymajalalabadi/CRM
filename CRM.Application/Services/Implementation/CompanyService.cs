using CRM.Application.Services.Interface;
using CRM.Domain.Entities.Companies;
using CRM.Domain.Interfaces;
using CRM.Domain.ViewModels.Company;

namespace CRM.Application.Services.Implementation
{
    public class CompanyService : ICompanyService
    {
        #region Constructor

        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        #endregion

        #region Methods

        public async Task<FilterCompanyViewModel> filterCompanyViewModel(FilterCompanyViewModel filter)
        {
            var query = await _companyRepository.GetCompanies();

            #region Filter

            if (!string.IsNullOrEmpty(filter.FilterCompanyName))
            {
                query = query.Where(c => c.Name.Contains(filter.FilterCompanyName));
            }

            if (!string.IsNullOrEmpty(filter.FilterCompanyCode))
            {
                query = query.Where(c => c.Code.Contains(filter.FilterCompanyCode));
            }

            #endregion

            query = query.OrderByDescending(c => c.CreateDate);

            #region Paging

            await filter.SetPaging(query);

            #endregion

            return filter;
        }


        public async Task<CreateCompanyResult> CreateCompany(CreateCompanyViewModel companyViewModel)
        {
            if (await _companyRepository.IsExistCompanyByMobilePhone(companyViewModel.MobilePhone))
            {
                return CreateCompanyResult.Fail;
            }

            var company = new Company()
            {
                Address = companyViewModel.Address,
                AgentName = companyViewModel.AgentName,
                City = companyViewModel.City,
                Description = companyViewModel.Description,
                Name = companyViewModel.Name,
                Code = companyViewModel.Code,
                MobilePhone = companyViewModel.MobilePhone,
                IntroduceName = companyViewModel.IntroduceName
            };

            await _companyRepository.AddCompany(company);
            await _companyRepository.SaveChanges();

            return CreateCompanyResult.Success;
        }

        public async Task<EditCompanyViewModel> GetCompanyByEdit(long companyId)
        {
            var company = await _companyRepository.GetCompanyById(companyId);

            if (company == null)
            {
                return null;
            }

            return new EditCompanyViewModel
            {
                Id = company.CompanyId,
                Name = company.Name,
                Description = company.Description,
                City = company.City,
                Code = company.Code,
                Address = company.Address,
                AgentName = company.AgentName,
                IntroduceName= company.IntroduceName,
                MobilePhone= company.MobilePhone,
            };
        }

        public async Task<EditCompanyResult> EditCompany(EditCompanyViewModel edit)
        {
            var company = await _companyRepository.GetCompanyById(edit.Id);

            if (company == null)
            {
                return EditCompanyResult.Fail;
            }

            company.Description = edit.Description;
            company.Address = edit.Address;
            company.AgentName = edit.AgentName;
            company.City = edit.City;
            company.Code = edit.Code;
            company.Name = edit.Name;
            company.MobilePhone = edit.MobilePhone;
            company.IntroduceName = edit.IntroduceName;

            _companyRepository.UpdateCompany(company);
            await _companyRepository.SaveChanges();

            return EditCompanyResult.Success;
        }

        public async Task<bool> DeleteCompany(long companyId)
        {
            var company = await _companyRepository.GetCompanyById(companyId);

            if (company == null)
            {
                return false;
            }

            company.IsDelete = true;

            _companyRepository.UpdateCompany(company);
            await _companyRepository.SaveChanges();

            return true;
        }

        #endregion
    }
}
