using CRM.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.Services.Interface
{
    public interface IProgramSetting
    {
        #region Methods

        Task<DashboardViewModel> FillDashboardViewModel(long userId);
        
        #endregion
    }
}
