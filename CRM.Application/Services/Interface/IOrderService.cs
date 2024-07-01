using CRM.Domain.ViewModels.Orders;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.Services.Interface
{
    public interface IOrderService
    {
        #region Methods

        Task<FilterOrderViewModel> FilterOrder(FilterOrderViewModel filter);

        Task<CreateOrderResult> CreateOrder(CreateOrderViewModel createOrder);

        Task<EditOrderViewModel> FillEditOrderViewModel(long orderId);

        Task<EditOrderResult> EditOrder(EditOrderViewModel editOrder);

        #endregion
    }
}
