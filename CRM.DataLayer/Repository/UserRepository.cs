using CRM.DataLayer.Context;
using CRM.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DataLayer.Repository
{
    public class UserRepository : IUserRepository
    {
        #region Constructor

        private CrmContext _context;

        public UserRepository(CrmContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods



        #endregion
    }
}
