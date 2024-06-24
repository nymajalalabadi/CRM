using CRM.Application.Services.Implementation;
using CRM.Application.Services.Interface;
using CRM.DataLayer.Repository;
using CRM.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.IoC
{
    public class DependencyContainer
    {
        public static void RejosterService(IServiceCollection services)
        {
            #region service

            services.AddScoped<IUserService, UserService>();

            #endregion

            #region repository

            services.AddScoped<IUserRepository, UserRepository>();

            #endregion
        }
    }
}
