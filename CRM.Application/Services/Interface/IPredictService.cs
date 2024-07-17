using CRM.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.Services.Interface
{
    public interface IPredictService
    {
        #region Methods

        Task<bool> ProcessMarketerPredict();

        Task DeleteAllMarketerPredict();

        Task<Marketer> GetMarketerPredict();

        #endregion
    }
}
