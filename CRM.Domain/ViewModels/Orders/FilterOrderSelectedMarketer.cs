using CRM.Domain.Entities.Orders;
using CRM.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.ViewModels.Orders
{
    public class FilterOrderSelectedMarketer : Paging<OrderSelectedMarketer>
    {
        public string? FilterMarketerName { get; set; }
        public string? FilterCustomerName { get; set; }
    }
}
