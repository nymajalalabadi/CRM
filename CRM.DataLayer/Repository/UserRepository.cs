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
            return _context.Users.Where(u => !u.IsDelete)
                .Include(u => u.Marketer)
                .Include(u => u.Customer)
                .AsQueryable();
        }

        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
        }

        public async Task<User?> GetUserById(long userId)
        {
            return await _context.Users.Where(u => !u.IsDelete).FirstOrDefaultAsync(u => u.UserId.Equals(userId));
        }

		public async Task<User?> GetUserDetailById(long userId)
        {
            return await _context.Users
                .Include(u => u.Marketer)
                .Include(u => u.Customer)
                .Where(u => !u.IsDelete)
                .FirstOrDefaultAsync(u => u.UserId.Equals(userId));
        }

        public async Task<User?> GetUserByUserName(string userName)
        {
            return await _context.Users.Where(c => !c.IsDelete).FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<bool> IsExistMarketerByUserName(string userName)
        {
            return await _context.Marketers.Include(m => m.User).AnyAsync(m => m.User.UserName.Equals(userName));
        }

        public async Task<Marketer?> GetMarketerById(long marketerId)
        {
            return await _context.Marketers.Where(u => !u.IsDelete).FirstOrDefaultAsync(m => m.UserId.Equals(marketerId));
        }

        public async Task AddMarketer(Marketer marketer)
        {
            await _context.Marketers.AddAsync(marketer);
        }

        public void UpdateMarketer(Marketer marketer)
        {
            _context.Marketers.Update(marketer);
        }

        public async Task<IQueryable<Marketer>> GetMarketerQueryable()
        {
             return _context.Marketers.Include(a => a.User).Where(m => !m.IsDelete).AsQueryable();
        }

        public async Task<bool> IsExistCustomerByUserName(string userName)
        {
            return await _context.Customers.Include(m => m.User).AnyAsync(m => m.User.UserName.Equals(userName));
        }

        public async Task AddCustomer(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
        }

        public async Task<Customer?> GetCustomerById(long customerId)
        {
            return await _context.Customers
                .Where(u => !u.IsDelete)
                .Include(u => u.User)
                .FirstOrDefaultAsync(c => c.UserId.Equals(customerId));  
        }

        public void UpdateCustomer(Customer customer)
        {
            _context?.Customers.Update(customer);   
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
