using CRM.Domain.Entities.Account;
using CRM.Domain.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Interfaces
{
    public interface IUserRepository
    {
        #region Methods

        Task<IQueryable<User>> GetAllUsers();
        
        Task AddUser(User user);

        void UpdateUser(User user);

        Task<User?> GetUserById(long userId);

        Task<User?> GetUserDetailById(long userId);

        Task<User?> GetUserByUserName(string userName);

        Task<bool> IsExistMarketerByUserName(string userName);

        Task<Marketer?> GetMarketerById(long marketerId);

		Task AddMarketer(Marketer marketer);

        void UpdateMarketer(Marketer marketer);

        Task<IQueryable<Marketer>> GetMarketerQueryable();

        Task<bool> IsExistCustomerByUserName(string userName);

        Task AddCustomer(Customer customer);

        Task<Customer?> GetCustomerById(long customerId);

        void UpdateCustomer(Customer customer);

        Task SaveChangeAsync();

        #endregion
    }
}
