using CRM.Domain.Entities.Orders;
using CRM.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.ViewModels.Orders
{
    public class FilterOrderViewModel : Paging<Order>
    {
        public string? FilterOrderName { get; set; }
        public string? FilterCustomerName { get; set; }
    }
}
