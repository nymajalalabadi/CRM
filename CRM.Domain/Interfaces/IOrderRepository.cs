using CRM.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Interfaces
{
    public interface IOrderRepository
    {
        #region Methods

        Task<Order?> GetOrderById(long orderId);

        Task AddOrder(Order order);

        void UpdateOrder(Order order);

        Task<IQueryable<Order>> GetOrders();

        Task SaveChange();

        #endregion
    }
}
