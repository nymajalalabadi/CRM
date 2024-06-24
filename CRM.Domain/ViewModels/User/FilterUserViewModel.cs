using CRM.Domain.Entities.Account;
using CRM.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.ViewModels.User
{
    public class FilterUserViewModel : Paging<Entities.Account.User>
    {
        public string? FilterMobile { get; set; }

        public string? FilterFirstName { get; set; }

        public string? FilterLastName { get; set; }
    }

}
