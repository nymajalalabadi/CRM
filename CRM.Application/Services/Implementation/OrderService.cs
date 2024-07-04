﻿using CRM.Application.Extensions;
using CRM.Application.Security;
using CRM.Application.Services.Interface;
using CRM.Application.StaticTools;
using CRM.Domain.Entities.Orders;
using CRM.Domain.Interfaces;
using CRM.Domain.ViewModels.Orders;
using Microsoft.EntityFrameworkCore;

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

        public async Task<EditOrderViewModel> FillEditOrderViewModel(long orderId)
        {
            var order = await _orderRepository.GetOrderById(orderId);

            if (order == null)
            {
                return null;
            }

            return new EditOrderViewModel()
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                Description = order.Description,
                Title = order.Title,
                ImageName= order.ImageName,
                OrderType= order.OrderType
            };
        }

        public async Task<EditOrderResult> EditOrder(EditOrderViewModel editOrder)
        {
            if (editOrder.ImageFile != null)
            {
                var orderAvatar = await _orderRepository.GetOrderById(editOrder.OrderId);

                if (orderAvatar == null)
                {
                    return EditOrderResult.Fail;
                }

                var orderImage = CodeGenerator.GenerateUniqCode() + Path.GetExtension(editOrder.ImageFile.FileName);
                editOrder.ImageFile.AddImageToServer(orderImage, FilePath.OrderImagePathServer, 280, 280, null, editOrder.ImageName!);

                orderAvatar.Title = editOrder.Title;
                orderAvatar.Description = editOrder.Description;
                orderAvatar.OrderType = editOrder.OrderType;
                orderAvatar.ImageName = orderImage;

                _orderRepository.UpdateOrder(orderAvatar);

                await _orderRepository.SaveChange();

                return EditOrderResult.Success;
            }

            var order = await _orderRepository.GetOrderById(editOrder.OrderId);

            if (order == null)
            {
                return EditOrderResult.Fail;
            }

            order.Title = editOrder.Title;
            order.Description = editOrder.Description;
            order.OrderType = editOrder.OrderType;

            _orderRepository.UpdateOrder(order);

            await _orderRepository.SaveChange();

            return EditOrderResult.Success;
        }

        public async Task<bool> DeleteOrder(long orderId)
        {
            var order = await _orderRepository.GetOrderById(orderId);

            if (order == null)
            {
                return false;
            }

            order.IsDelete = true;

            _orderRepository.UpdateOrder(order);

            await _orderRepository.SaveChange();

            return true;
        }

        public async Task<AddOrderSelectMarketerResult> AddOrderSelectMarketer(OrderSelectMarketerViewModel order, long userId)
        {
            var currentOrder = await _orderRepository.GetOrderById(order.OrderId);

            if (currentOrder == null) 
            { 
                return AddOrderSelectMarketerResult.Fail;
            }

            if (await _orderRepository.IsExistOrderSelectedMarketer(order.OrderId))
            {
                return AddOrderSelectMarketerResult.SelectedMarketerExist;
            }

            var selectedMarketer = new OrderSelectedMarketer()
            {
                OrderId = order.OrderId,
                MarketerId = order.MarketerId,
                ModifyUserId = userId,
                Description = order.Description
            };

            await _orderRepository.AddOrderSelectedMarketer(selectedMarketer);

            await _orderRepository.SaveChange();

            return AddOrderSelectMarketerResult.Success;
        }

        public async Task<FilterOrderSelectedMarketer> FilterOrderSelectedMarketer(FilterOrderSelectedMarketer filter)
        {
            var query = await _orderRepository.GetOrderSelectedMarketers();

            #region Filter

            if (!string.IsNullOrEmpty(filter.FilterCustomerName))
            {
                query = query.Where(a =>
                    EF.Functions.Like(a.Order.Customer.User.FirstName, $"%{filter.FilterCustomerName}%") ||
                    EF.Functions.Like(a.Order.Customer.User.LastName, $"%{filter.FilterCustomerName}%") ||
                    EF.Functions.Like(a.Order.Customer.User.FirstName + " " + a.Order.Customer.User.LastName, 
                    $"%{filter.FilterCustomerName}%"));
            }

            if (!string.IsNullOrEmpty(filter.FilterMarketerName))
            {
                query = query.Where(a => 
                EF.Functions.Like(a.Marketer.User.FirstName, $"%{filter.FilterMarketerName}%") ||
                EF.Functions.Like(a.Marketer.User.LastName, $"%{filter.FilterMarketerName}%") ||
                EF.Functions.Like(a.Marketer.User.FirstName + " " + a.Marketer.User.LastName, 
                    $"%{filter.FilterMarketerName}%"));
            }

            #endregion

            query = query.OrderByDescending(a => a.CreateDate);

            #region Paging

            await filter.SetPaging(query);

            #endregion

            return filter;
        }

        public async Task<bool> DeleteOrderSelectedMarketer(long orderId)
        {
            var orderSelectedMarketer = await _orderRepository.GetOrderSelectedMarketerById(orderId);

            if (orderSelectedMarketer == null)
            {
                return false;
            }

            orderSelectedMarketer.IsDelete = true;

            _orderRepository.DeleteOrderSelectedMarketer(orderSelectedMarketer);
            await _orderRepository.SaveChange();

            return true;
        }

        #endregion
    }
}
