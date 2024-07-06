using CRM.Application.Services.Interface;
using CRM.Domain.Interfaces;
using CRM.Domain.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.Services.Implementation
{
    public class ProgramSetting : IProgramSetting
    {
        #region Constructor

        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IOrderRepository _orderRepository;

        public ProgramSetting(IUserRepository userRepository, ICompanyRepository companyRepository, IOrderRepository orderRepository)
        {
            _userRepository = userRepository;
            _companyRepository = companyRepository;
            _orderRepository = orderRepository;
        }

        #endregion

        #region Methods

        public async Task<DashboardViewModel> FillDashboardViewModel(long userId)
        {
            var allUsers = await _userRepository.GetAllUsers();
            var allCompany = await _companyRepository.GetCompanies();
            var allorders = await _orderRepository.GetOrders();

            var result = new DashboardViewModel()
            {
                OrderCount = allorders.Count(o => !o.IsDelete),
                IsOrderFinishCount = allorders.Count(o => !o.IsDelete && o.IsFinish == true),
                CompanyCount = allCompany.Count(c => !c.IsDelete),
                MarketerCount = allUsers.Count(m => !m.Marketer.IsDelete && m.Marketer != null),
                CustomerCount = allUsers.Count(c => !c.Customer.IsDelete && c.Customer != null),
                TodayCustomerCount = allUsers.Count(c => !c.Customer.IsDelete && c.Customer != null && 
                c.CreateDate.Day == DateTime.Now.Day ),
                SelfUser = allUsers.FirstOrDefault(u => u.UserId == userId)!
            };

            return result;
        }

        #endregion
    }
}
