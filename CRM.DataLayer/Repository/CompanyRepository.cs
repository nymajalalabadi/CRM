using CRM.DataLayer.Context;
using CRM.Domain.Interfaces;
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



        #endregion
    }
}
