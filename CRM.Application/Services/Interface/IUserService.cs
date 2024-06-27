using CRM.Domain.ViewModels.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.Services.Interface
{
    public interface IUserService
    {
        #region Methods

        Task<FilterUserViewModel> FilterUser(FilterUserViewModel filter);

        Task<AddMarketerResult> AddMarketer(AddMarketerViewModel marketer);

		Task<EditMarketerViewModel> GetMarketerForEdit(long marketerId);

		Task<EditMarketerResult> EditMarketer(EditMarketerViewModel marketer);

		#endregion
	}
}
