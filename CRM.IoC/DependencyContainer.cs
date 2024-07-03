using CRM.Application.Services.Implementation;
using CRM.Application.Services.Interface;
using CRM.DataLayer.Repository;
using CRM.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.IoC
{
    public class DependencyContainer
    {
        public static void RejosterService(IServiceCollection services)
        {
            #region service

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICompanyService, CompanyService>();

            #endregion

            #region repository

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();

            #endregion
        }
    }
}
