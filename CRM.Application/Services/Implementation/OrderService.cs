using CRM.Application.Extensions;
using CRM.Application.Security;
using CRM.Application.Services.Interface;
using CRM.Application.StaticTools;
using CRM.Domain.Entities.Orders;
using CRM.Domain.Interfaces;
using CRM.Domain.ViewModels.Orders;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.Services.Implementation
{
    public class OrderService : IOrderService
    {
        #region Constructor

        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository) 
        { 
            _orderRepository = orderRepository;
        }

        #endregion

        #region Methods

        public async Task<FilterOrderViewModel> FilterOrder(FilterOrderViewModel filter)
        {
            var query = await _orderRepository.GetOrders();

            #region Filter

            if (!string.IsNullOrEmpty(filter.FilterOrderName))
            {
                query = query.Where(o => o.Title.Contains(filter.FilterOrderName));
            }

            if (!string.IsNullOrEmpty(filter.FilterCustomerName))
            {
                query = query.Where(a =>
                    EF.Functions.Like(a.Customer.User.FirstName, $"%{filter.FilterCustomerName}%") ||
                    EF.Functions.Like(a.Customer.User.LastName, $"%{filter.FilterCustomerName}%") ||
                    EF.Functions.Like(a.Customer.User.FirstName + " " + a.Customer.User.LastName, $"%{filter.FilterCustomerName}%"));
            }

            #endregion

            query = query.OrderByDescending(o => o.CreateDate);

            #region Paging

            await filter.SetPaging(query);

            #endregion

            return filter;
        }

        public async Task<CreateOrderResult> CreateOrder(CreateOrderViewModel createOrder)
        {
            if (createOrder.ImageFile != null)
            {
                var orderImage = CodeGenerator.GenerateUniqCode() + Path.GetExtension(createOrder.ImageFile.FileName);
                createOrder.ImageFile.AddImageToServer(orderImage, FilePath.OrderImagePathServer, 280, 280);

                var orderAvatar = new Order()
                {
                    CustomerId = createOrder.CustomerId,
                    Description = createOrder.Description,
                    OrderType = createOrder.OrderType,
                    Title = createOrder.Title,
                    ImageName = orderImage
                };


                await _orderRepository.AddOrder(orderAvatar);
                await _orderRepository.SaveChange();

                return CreateOrderResult.Success;
            }

            var order = new Order()
            {
                CustomerId = createOrder.CustomerId,
                Description = createOrder.Description,
                OrderType = createOrder.OrderType,
                Title = createOrder.Title,
            };


            await _orderRepository.AddOrder(order);
            await _orderRepository.SaveChange();

            return CreateOrderResult.Success;
        }

        #endregion
    }
}
