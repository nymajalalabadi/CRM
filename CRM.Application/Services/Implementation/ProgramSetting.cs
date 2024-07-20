using CRM.Application.Services.Interface;
using CRM.Domain.Interfaces;
using CRM.Domain.ViewModels.Common;
using Microsoft.EntityFrameworkCore;

namespace CRM.Application.Services.Implementation
{
    public class ProgramSetting : IProgramSetting
    {
        #region Constructor

        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ILeadRepository _leadRepository;

        public ProgramSetting(IUserRepository userRepository, ICompanyRepository companyRepository, IOrderRepository orderRepository, ILeadRepository leadRepository)
        {
            _userRepository = userRepository;
            _companyRepository = companyRepository;
            _orderRepository = orderRepository;
            _leadRepository = leadRepository;
        }

        #endregion

        #region Methods

        public async Task<DashboardViewModel> FillDashboardViewModel(long userId)
        {
            var allUsers = await _userRepository.GetAllUsers();
            var allCompany = await _companyRepository.GetCompanies();
            var allOrders = await _orderRepository.GetOrders();
            var allLeads = await _leadRepository.GetLeads();

            var orderPerMonth = new List<int>();

            var orderQueryable = await _orderRepository.GetOrders();

            for (int i = 0; i < 5; i++)
            {
                var ordersCount = orderQueryable.Count(a => a.CreateDate.Month == DateTime.Now.AddMonths(-i).Month);
                orderPerMonth.Add(ordersCount);
            }


            var result = new DashboardViewModel()
            {
                OrderCount = allOrders.Count(o => !o.IsDelete),
                IsOrderFinishCount = allOrders.Count(o => !o.IsDelete && o.IsFinish == true),
                CompanyCount = allCompany.Count(c => !c.IsDelete),
                MarketerCount = allUsers.Count(m => !m.Marketer.IsDelete && m.Marketer != null),
                CustomerCount = allUsers.Count(c => !c.Customer.IsDelete && c.Customer != null),
                TodayCustomerCount = allUsers.Count(c => !c.Customer.IsDelete && c.Customer != null && 
                c.CreateDate.Day == DateTime.Now.Day ),
                UserOpenLeadCount = allLeads.Count(l => !l.IsDelete && l.CreatedById == userId && 
                l.LeadStatus == Domain.Entities.Leads.LeadStatus.Active),
                SelfUser = allUsers.FirstOrDefault(u => u.UserId == userId)!,
                OrderCountPerMonth = orderPerMonth,
            };

            return result;
        }

        #endregion
    }
}
