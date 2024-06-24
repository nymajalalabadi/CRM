using CRM.DataLayer.Context;
using CRM.Domain.Entities.Account;
using CRM.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IQueryable<User>> GetAllUsers()
        {
            return _context.Users.Where(u => !u.IsDelete).Include(u => u.Marketer).AsQueryable();
        }

        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<User?> GetUserById(long userId)
        {
            return await _context.Users.Where(u => !u.IsDelete).FirstOrDefaultAsync(u => u.UserId.Equals(userId));
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
        }

        public async Task AddMarketer(Marketer marketer)
        {
            await _context.Marketers.AddAsync(marketer);
        }

        public void UpdateMarketer(Marketer marketer)
        {
            _context.Marketers.Update(marketer);
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
