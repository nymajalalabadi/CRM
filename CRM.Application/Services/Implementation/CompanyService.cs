using CRM.Application.Services.Interface;
using CRM.Domain.Entities.Companies;
using CRM.Domain.Interfaces;
using CRM.Domain.ViewModels.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        #endregion
    }
}
