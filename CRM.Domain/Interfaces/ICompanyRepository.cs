using CRM.Domain.Entities.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Interfaces
{
    public interface ICompanyRepository
    {
        #region Methods

        Task<IQueryable<Company>> GetCompanies();

        Task AddCompany(Company company);

        Task<Company?> GetCompanyById(long companyId);

        Task<bool> IsExistCompanyByMobilePhone(string MobilePhone);

        void UpdateCompany(Company company);

        Task SaveChanges();

        #endregion
    }
}
