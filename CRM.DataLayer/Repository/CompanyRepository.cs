using CRM.DataLayer.Context;
using CRM.Domain.Entities.Companies;
using CRM.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DataLayer.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        #region Constructor

        private CrmContext _context;

        public CompanyRepository(CrmContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        public async Task<IQueryable<Company>> GetCompanies()
        {
            return _context.Companies.Where(c => !c.IsDelete).AsQueryable();
        }

        public async Task AddCompany(Company company)
        {
            await _context.Companies.AddAsync(company);
        }

        public async Task<Company?> GetCompanyById(long companyId)
        {
            return await _context.Companies.Where(c => !c.IsDelete).FirstOrDefaultAsync(c => c.CompanyId == companyId);
        }

        public async Task<bool> IsExistCompanyByMobilePhone(string MobilePhone)
        {
            return await _context.Companies.AnyAsync(c => c.MobilePhone.Equals(MobilePhone));
        }

        public void UpdateCompany(Company company)
        {
            _context.Companies.Update(company);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
