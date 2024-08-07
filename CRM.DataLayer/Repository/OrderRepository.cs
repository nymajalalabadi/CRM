﻿using CRM.DataLayer.Context;
using CRM.Domain.Entities.Orders;
using CRM.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DataLayer.Repository
{
    public class OrderRepository : IOrderRepository
    {
        #region Constructor

        private CrmContext _context;

        public OrderRepository(CrmContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        public async Task<Order?> GetOrderById(long orderId)
        {
            return await _context.Orders.Where(o => !o.IsDelete).FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task AddOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
        }

        public async Task<IQueryable<Order>> GetOrders()
        {
            return _context.Orders.Where(o => !o.IsDelete)
                .Include(a => a.Customer)
                .ThenInclude(a => a.User)
                .AsQueryable();
        }

        public async Task<IQueryable<OrderSelectedMarketer>> GetOrderSelectedMarketers()
        {
            return _context.orderSelectedMarketers.Where(o => !o.IsDelete)
                .Include(a => a.Order)
                .ThenInclude(a => a.Customer)
                .ThenInclude(a => a.User)
                .Include(a => a.Marketer)
                .ThenInclude(a => a.User)
                .AsQueryable();
        }

        public async Task<bool> IsExistOrderSelectedMarketer(long orderId)
        {
            return await _context.orderSelectedMarketers.AnyAsync(o => o.OrderId.Equals(orderId));
        }

        public async Task AddOrderSelectedMarketer(OrderSelectedMarketer orderSelectedMarketer)
        {
            await _context.orderSelectedMarketers.AddAsync(orderSelectedMarketer);
        }

        public async Task<OrderSelectedMarketer?> GetOrderSelectedMarketerById(long orderId)
        {
            return await _context.orderSelectedMarketers.Where(o => !o.IsDelete).FirstOrDefaultAsync(s => s.OrderId.Equals(orderId));
        }

        public void UpdateOrderSelectedMarketer(OrderSelectedMarketer orderSelectedMarketer)
        {
            _context.orderSelectedMarketers.Update(orderSelectedMarketer);
        }

        public void DeleteOrderSelectedMarketer(OrderSelectedMarketer orderSelectedMarketer)
        {
             _context.orderSelectedMarketers.Remove(orderSelectedMarketer);
        }

        public async Task SaveChange()
        {
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
